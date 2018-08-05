using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspnetIdentityFromScratch.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Role { get; set; }
    }
}