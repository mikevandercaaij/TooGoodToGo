namespace Core.Domain.Entities
{
    public class CanteenEmployee
    {
        [Key]
        private int ProductId { get; set; }

        [Required(ErrorMessage = "Vul je naam in!")]
        private string? Name { get; set; }

        [Required(ErrorMessage = "Vul je personeelsnummer in!")]
        private string? EmployeeId { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        private string? Location { get; set; }
    }
}
