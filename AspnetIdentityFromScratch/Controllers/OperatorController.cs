using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspnetIdentityFromScratch.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.Security;
using AspnetIdentityFromScratch.ViewModels;
using Microsoft.AspNet.Identity;

namespace AspnetIdentityFromScratch.Controllers
{
    public class OperatorController : Controller
    {
        
        
        // GET: Operator
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(string sidx, string sord, int page, int rows, string searchString, string searchField,
            string searchOper)
        {
            var op = HttpContext.User.Identity.Name;
            var db = HttpContext.GetOwinContext().Get<ApplicationContext>().SupportRequests;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = db.Where(c=>c.Operator==op).Select(
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


            //todo change sort
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

        public ActionResult CreateRequest()
        {
            var crRqModel = new CreateRequestVmodel();

            var items = HttpContext.GetOwinContext().Get<ApplicationContext>().RequestCategories
                .Select(c => new SelectListItem() {Value = c.CategoryName, Text = c.CategoryName}).ToList();

            crRqModel.Categories = new SelectList(items, "Value", "Text");
            return View(crRqModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRequest(CreateRequestVmodel model)
        {
             var items = HttpContext.GetOwinContext().Get<ApplicationContext>().RequestCategories
               .Select(c => new SelectListItem() { Value = c.CategoryName, Text = c.CategoryName }).ToList();

            model.Categories = new SelectList(items, "Value", "Text");
            if (ModelState.IsValid)
            {
                // var items = HttpContext.GetOwinContext().Get<ApplicationContext>().RequestCategories
                //    .Select(c => new SelectListItem() { Value = c.CategoryName, Text = c.CategoryName }).ToList();

                //model.Categories = new SelectList(items, "Value", "Text");

                var db = HttpContext.GetOwinContext().Get<ApplicationContext>();
                SupportRequest sr = new SupportRequest();
                sr.Category = model.Category;
                sr.Phone = model.Phone;
                sr.Operator = HttpContext.User.Identity.Name;
                sr.RqText = model.Text;
                sr.State = "Зарегистрирован";
                sr.Time = DateTime.Now;
                bool _success = true;
                try
                {
                    db.SupportRequests.Add(sr);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                    _success = false;
                }
               
                if (_success)
                {
                    return RedirectToAction("Index", "Operator");
                }
            }
            return View(model);
        }
    }
}