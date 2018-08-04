using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspnetIdentityFromScratch.Models
{
    
    public class SupportRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Category { get; set; }
        public string RqText { get; set; }
        public string ExecutorComment { get; set; }
        public DateTime Time { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Executor { get; set; }
    }
}