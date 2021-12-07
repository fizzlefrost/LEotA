using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PatronomicName { get; set; }
        public string EmbedLink { get; set; }
        public object Password { get; set; }
    }
    
    public class LoginViewModel
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string PasswordConfirm { get; set; }
        
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        [JsonPropertyName("returnUrl")]
        public string ReturnUrl { get; set; }
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
        
        [Required]
        [DataType(DataType.Password)]  
        [JsonPropertyName("confirmPassword")]
        [Display(Name = "Confirm password")]  
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]  
        public string ConfirmPassword { get; set; }
    }

    public class EngineRegisterModel
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        
        [JsonPropertyName("patronimicName")]
        public string? PatronomicName { get; set; }
        
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
    

    public class EngineRegisteredViewModel
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
        [EmailAddress]
        [JsonPropertyName("roles")]
        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }
    }
}