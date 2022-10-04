﻿namespace Core.Domain.Entities
{
    public class CanteenEmployee
    {
        [Key]
        public int CanteenEmployeeId { get; set; }

        [Required(ErrorMessage = "Vul je voornaam in!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vul je achternaam in!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Vul je personeelsnummer in!")]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        public string? Location { get; set; }
    }
}
