﻿using System.ComponentModel.DataAnnotations;

namespace LEotA.Engine.Web.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
