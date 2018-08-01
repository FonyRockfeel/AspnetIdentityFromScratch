using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspnetIdentityFromScratch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AspnetIdentityFromScratch.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        private ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        private List<string> Roles=> RoleManager.Roles.Select(r => r.Name).ToList();

        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
           
            ViewBag.Roles = Roles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, model.Role);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await UserManager.DeleteAsync(user);
                    foreach (string err in result.Errors)
                    {
                        ModelState.AddModelError("",err);
                    }
                }
            }
            ViewBag.Roles = Roles;
            return View(model);
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string retUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, shouldLockout: false);
                    if (result == SignInStatus.Success)
                    {
                        if (String.IsNullOrEmpty(retUrl))
                            return RedirectToAction("Index", "Home");
                        return Redirect(retUrl);
                    }
                   
                }
            }
            ViewBag.retUrl = retUrl;
            return View(model);
        }
        public ActionResult Logoff()
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}