using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetIdentityFromScratch.Models
{
    
    public class SupportRequest
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Category { get; set; }
        public string Text { get; set; }
        public string ExecutorComment { get; set; }
        public DateTime Time { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Executor { get; set; }
    }
}