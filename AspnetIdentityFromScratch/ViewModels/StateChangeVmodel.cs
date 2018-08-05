using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspnetIdentityFromScratch.ViewModels
{
    public class StateChangeVmodel
    {
        public Guid Id { get; set; }
     
        [Display(Name = "ФИО клиента")]
        public string ClientName { get; set; }

       
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Категория обращения")]
        public string Category { get; set; }

     
        [Display(Name = "Текст обращения")]
        public string Text { get; set; }

        
        [Display(Name = "Время регистрации обращения")]
        public DateTime Time { get; set; }


        [Required(ErrorMessage = "Введите состояние")]
        [Display(Name = "Состояние обращения")]
        public string State { get; set; }

        [Required(ErrorMessage = "Введите комментарий")]
        [Display(Name = "Комментарий исполнителя")]
        public string Comment { get; set; }
    }
}