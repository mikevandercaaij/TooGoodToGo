using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models.CanteenModels
{
    public class CanteenEmployeeRegisterModel
    {
        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Vul je voornaam in!")]
        public string? FirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Vul je achternaam in!")]
        public string? LastName { get; set; }

        [Display(Name = "Personeelsnummer")]
        [Required(ErrorMessage = "Vul je personeelsnummer in!")]
        public string? EmployeeId { get; set; }

        [Display(Name = "Kantine")]
        [Required(ErrorMessage = "Vul een locatie in!")]
        public CanteenLocationEnum? Location { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "Vul je wachtwoord in!")]
        public string? Password { get; set; }
    }
}