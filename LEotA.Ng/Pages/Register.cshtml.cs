﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient;
using LEotA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LEotA.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly EngineClientManager _engineClient;
        private readonly EngineAuthenticationManager _engineAuthentication;
        
        public RegisterModel(EngineClientManager _engineClient, EngineAuthenticationManager _engineAuthentication)
        {
            this._engineClient = _engineClient;
            this._engineAuthentication = _engineAuthentication;
        }
        
        public class RegisterViewModel
        {
            [Required]
            [JsonPropertyName("firstName")]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }
        
            [Required]
            [JsonPropertyName("lastName")]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
        
            [JsonPropertyName("patronimicName")]
            [Display(Name = "PatronimicName")]
            public string? PatronomicName { get; set; }
        
            [JsonPropertyName("embedLink")]
            [Display(Name = "EmbedLink")]
            public string EmbedLink { get; set; }
        
            [Required]
            [EmailAddress]
            [JsonPropertyName("email")]
            [Display(Name = "Email")]
            public string Email { get; set; }
        
            [Required]
            [JsonPropertyName("password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]  
            [DataType(DataType.Password)]  
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        
            [DataType(DataType.Password)]  
            [Display(Name = "Confirm password")]  
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]  
            public string ConfirmPassword { get; set; }
        }
        
        public string ReturnUrl { get; set; }  
        [BindProperty]  
        public RegisterViewModel Input { get; set; }
        
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");  
            if (ModelState.IsValid)  
            {  
                var user = new EngineRegisterModel()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PatronomicName = Input.PatronomicName,
                    EmbedLink = Input.EmbedLink,
                    Email = Input.Email
                };  
                var result = await _engineAuthentication.RegisterAsync(user, Input.Password);
                foreach (var error in result.Logs)  
                {  
                    ModelState.AddModelError(string.Empty, error);  
                }  
            }  
  
            // If we got this far, something failed, redisplay form  
            return Page();  
        }
    }
}