using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspnetIdentityFromScratch.Models;
using AspnetIdentityFromScratch.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;

namespace AspnetIdentityFromScratch.Controllers
{
    public class ExecutorController : Controller
    {
        [Authorize(Roles = "executor")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "executor")]
        public JsonResult GetData(string sidx, string sord, int page, int rows, string searchString, string searchField,
            string searchOper)
        {
            var op = HttpContext.User.Identity.Name;
            var db = HttpContext.GetOwinContext().Get<ApplicationContext>().SupportRequests;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = db.Select(
                a => new
                {
                    a.ClientName,
                    a.Operator,
                    a.Executor,
                    a.ExecutorComment,
                    a.State,
                    a.RqText,
                    a.Time,
                    a.Category,
                    a.Id,
                    a.Phone
                });
            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);


            //todo change sort logics
            var s = $"it.{sidx} {sord}";
            results = results.OrderBy(s);
            results = results.Skip(pageIndex * pageSize).Take(pageSize);


            if (!string.IsNullOrEmpty(searchString) & !string.IsNullOrEmpty(searchField) &
                !string.IsNullOrEmpty(searchOper))
            {
                if (searchOper == "eq")
                    results = results.Where($"{searchField}=@0", searchString);
                else if (searchOper == "ne")
                    results = results.Where($"{searchField}!=@0", searchString);
                else
                    results = results.Where($"{searchField}.Contains(@0)", searchString);
            }

            var jsondata = new
            {
                total = totalPages,
                page,
                records = db.Count(),
                rows = results
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "executor")]
        public JsonResult GetDetails(Guid? id)
        {
            if (id == null || id == default(Guid))
                return null;
            var db = HttpContext.GetOwinContext().Get<ApplicationContext>().SupportRequests;
            var item = db.First(s => s.Id == id);
            return Json(new string[] {item.RqText, item.ExecutorComment}, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "executor")]
        public ActionResult EditRequest(Guid? selectedId)
        {
            if (selectedId==null||selectedId == default(Guid))
               return Redirect("Index");
          
           
            var op = HttpContext.User.Identity.Name;
            var db = HttpContext.GetOwinContext().Get<ApplicationContext>().SupportRequests;
            var item = db.First(c=>c.Id==selectedId);
            var crRqModel = new StateChangeVmodel()
            {
                Id = item.Id,
                Phone = item.Phone,
                Category = item.Category,
                ClientName = item.ClientName,
                State = item.State,
                Text = item.RqText,
                Time = item.Time
            };
            return View(crRqModel);
        }

        [HttpPost]
        [Authorize(Roles = "executor")]
        public async Task<ActionResult> EditRequest(StateChangeVmodel model)
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationContext>();
            var item = db.SupportRequests.First(c => c.Id == model.Id);
            if (ModelState.IsValid)
            {
                var op = HttpContext.User.Identity.Name;
                item.State = model.State;
                item.ExecutorComment = model.Comment;
                item.Executor = op;
                bool _success = true;
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _success = false;
                }

                if (_success)
                {
                    return RedirectToAction("Index", "Executor");
                }
            }

            model.ClientName = item.ClientName;
            model.Phone = item.Phone;
            model.Category = item.Category;
            model.Text = item.RqText;
            model.Time = item.Time;

            return View(model);
        }

    }
}