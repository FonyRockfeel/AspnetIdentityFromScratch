using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

namespace AspnetIdentityFromScratch.Models
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("ApplicationDB")
        {
            
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public IDbSet<SupportRequest> SupportRequests { get; set; }
        public IDbSet<RqCategory> RequestCategories { get; set; }
    }
}