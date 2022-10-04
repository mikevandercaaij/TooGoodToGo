using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Identificatienummer is verplicht!")]
        public string? UserId { get; set; }
        
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        [UIHint("password")]
        public string? Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
