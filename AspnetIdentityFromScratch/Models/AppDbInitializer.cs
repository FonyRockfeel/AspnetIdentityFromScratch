using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspnetIdentityFromScratch.Models
{
    public class AppDbInitializer:DropCreateDatabaseAlways<ApplicationContext>
    {
      
        //public override void InitializeDatabase(ApplicationContext context)
        //{
        //    //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,$"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
        //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, $"USE master DROP DATABASE [{context.Database.Connection.Database}]");

        //    base.InitializeDatabase(context);
        //    //_innerInitializer.InitializeDatabase(context);
           
        //}
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

            //#region Init users

            //var admin = new ApplicationUser()
            //{
            //    Email = "overmind@gmail.com",
            //    UserName = "Overmind"
            //};

            //var oper = new ApplicationUser()
            //{
            //    Email = "morgot@gmail.com",
            //    UserName = "Morgot"
            //};
            //var exec = new ApplicationUser()
            //{
            //    Email = "solo@gmail.com",
            //    UserName = "Solo"
            //};
            //var passw = "123";

            //var res = userManager.Create(admin, passw);
            //if (res.Succeeded)
            //{
            //    userManager.AddToRole(admin.Id, roleAdmin.Name);
            //}
            //else
            //{
            //    Debug.WriteLine("init false");
            //}

            //res = userManager.Create(oper, passw);
            //if (res.Succeeded)
            //{
            //    userManager.AddToRole(oper.Id, roleOperator.Name);
            //}
            //else
            //{
            //    Debug.WriteLine("init false");
            //}

            //res = userManager.Create(exec, passw);
            //if (res.Succeeded)
            //{
            //    userManager.AddToRole(exec.Id, roleExecutor.Name);
            //}
            //#endregion


            //todo init
            for (int i = 0; i < 10; i++)
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
                    Phone = "80000000000"
                });
            }
            context.RequestCategories.Add(new RqCategory() {CategoryName = "Общие вопросы"});
            context.RequestCategories.Add(new RqCategory() {CategoryName = "Технические проблемы"});
            
            base.Seed(context);
        }
    }
}