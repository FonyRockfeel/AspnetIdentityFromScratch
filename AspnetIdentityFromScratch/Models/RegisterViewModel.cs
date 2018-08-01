using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspnetIdentityFromScratch.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Псевдоним")]
        public string Username { get; set; }

        [Required]
        [Display(Name="Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name="Роль пользователя")]
        public string Role { get; set; }

        [Required]
        [Display(Name="Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

    }
}