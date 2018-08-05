using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspnetIdentityFromScratch.Models
{
    public class AppDbInitializer: DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            var roleAdmin = new IdentityRole() { Name = "admin" };
            var roleOperator = new IdentityRole() { Name = "operator" };
            var roleExecutor = new IdentityRole() { Name = "executor" };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleOperator);
            roleManager.Create(roleExecutor);

            var admin = new ApplicationUser()
            {
                Email = "overmind@gmail.com",
                UserName = "Overmind"
            };

            var passw = "hamster";
            var res = userManager.Create(admin, passw);

            if (res.Succeeded)
            {
                userManager.AddToRole(admin.Id, roleAdmin.Name);
            }
            //todo init
            for (int i = 0; i < 60; i++)
            {
                context.SupportRequests.Add(new SupportRequest
                {
                    ClientName = "client" + i,
                    Executor = "Exec" + i,
                    Operator = "Operator" + i,
                    ExecutorComment = "sdfs",
                    State = "Зарегистрирован",
                    RqText = "sdf",
                    Time = DateTime.Now,
                    Category = "Общие вопросы",
                    Phone = ""
                });
            }
            context.RequestCategories.Add(new RqCategory() {CategoryName = "Общие вопросы"});
            context.RequestCategories.Add(new RqCategory() {CategoryName = "Технические проблемы"});
            
            base.Seed(context);
        }
    }
}