namespace Core.Domain.Entities
{
    public class CanteenEmployee
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Vul je naam in!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vul je personeelsnummer in!")]
        public string? EmployeeId { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        public string? Location { get; set; }
    }
}
