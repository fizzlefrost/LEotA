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
        [JsonPropertyName("email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [JsonPropertyName("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [JsonPropertyName("defaultReturnUrl")] public string DefaultReturnUrl = "AdminPanel";
    }
}