namespace Core.Domain.Entities
{
    public class Student
    {
        [Key]
        private int StudentId { get; set; }

        [Required(ErrorMessage = "Vul je naam in")]
        private string? Name { get; set; }

        [Required(ErrorMessage = "Vul je geboortedatum in!")]
        private DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vul je studentennummer in!")]
        private int? StudentNumber { get; set; }

        [Required(ErrorMessage = "Vul je email in!")]
        [EmailAddress]
        private string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Geef aan in welke stad je studeert!")]
        private CityEnum? StudyCity { get; set; }
        
        [Required(ErrorMessage = "Vul je telefoonnummer in!")]
        [Phone]
        private string? PhoneNumber { get; set; }         
        
    }
}
