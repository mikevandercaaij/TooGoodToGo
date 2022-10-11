using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class StudentRegisterModel
    {
        [Required(ErrorMessage = "Vul je voornaam in")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vul je achternaam in")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Vul je geboortedatum in!")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vul je studentennummer in!")]
        public string? StudentNumber { get; set; }

        [Required(ErrorMessage = "Vul je email in!")]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Geef aan in welke stad je studeert!")]
        public CityEnum? StudyCity { get; set; }

        [Required(ErrorMessage = "Vul je telefoonnummer in!")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vul je wachtwoord in!")]
        public string? Password { get; set; }
    }
}