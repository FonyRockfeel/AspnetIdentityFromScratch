using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspnetIdentityFromScratch.ViewModels
{
    public class StateChangeVmodel
    {
        public string ClientName { get; set; }
        public string Category { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Введите состояние")]
        public string State { get; set; }
        [Required(ErrorMessage = "Введите комментарий")]
        public string Comment { get; set; }
    }
}