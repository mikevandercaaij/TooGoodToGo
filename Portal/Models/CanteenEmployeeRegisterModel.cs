using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class CanteenEmployeeRegisterModel
    {
        [Required(ErrorMessage = "Vul je voornaam in!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vul je achternaam in!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Vul je personeelsnummer in!")]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Vul je wachtwoord in!")]
        public string? Password { get; set; }
    }
}