using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspnetIdentityFromScratch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AspnetIdentityFromScratch.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        // GET: Admin
        public ActionResult Index()
        {
           
            var list = UserManager.Users.ToList().Select(user => new UserListItem{UserName=user.UserName, Roles=UserManager.GetRoles(user.Id)} ).ToList();
           
            ViewBag.Users = list;
            return View();
        }
    }

    public class UserListItem
    {
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}