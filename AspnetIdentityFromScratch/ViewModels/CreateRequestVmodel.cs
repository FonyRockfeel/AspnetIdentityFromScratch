using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetIdentityFromScratch.ViewModels
{
    public class CreateRequestVmodel
    {
        [Required(ErrorMessage = "поле ФИО клиента не может быть пустым")]
        [Display(Name = "ФИО клиента")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Опишите проблему")]
        [Display(Name = "Текст обращения")]
        public string Text { get; set; }
        [Required(ErrorMessage = "введите номер тетелефона клиента")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный формат номера телефона")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Выберите категорию обращения")]
        [Display(Name = "Категория обращения")]
        public string Category
        {
            //get { return SelectedCategory.Value; }
            get;
            set;
        }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}