using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class StudentRegisterModel
    {
        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Vul je voornaam in")]

        public string? FirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Vul je achternaam in")]
        public string? LastName { get; set; }

        [Display(Name = "Geboortedatum")]
        [Required(ErrorMessage = "Vul je geboortedatum in!")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Studentennummer")]
        [Required(ErrorMessage = "Vul je studentennummer in!")]
        public string? StudentNumber { get; set; }

        [Display(Name = "E-mailadres")]
        [Required(ErrorMessage = "Vul je email in!")]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Display(Name = "Studiestad")]
        [Required(ErrorMessage = "Geef aan in welke stad je studeert!")]
        public CityEnum? StudyCity { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "Vul je telefoonnummer in!")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "Vul je wachtwoord in!")]
        public string? Password { get; set; }
    }
}