using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

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
    }
}