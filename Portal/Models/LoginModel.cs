using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class LoginModel
    {
        [Display(Name = "Studenten/Personeelsnummer")]
        [Required(ErrorMessage = "Identificatienummer is verplicht!")]
        public string? UserId { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        [UIHint("password")]
        public string? Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
