namespace Core.Domain.Entities
{
    public class Product
    {
        [Key]
        private int ProductId { get; set; }

        [Required(ErrorMessage = "Vul een naam voor het pakket in!")]
        private string? Name { get; set; }

        [Required(ErrorMessage = "Geef aan of het product alcohol bevat!")]
        private bool? ContainsAlcohol { get; set; }

        [Required(ErrorMessage = "Je moet een foto van het product meegeven!")]
        private byte[]? Picture { get; set; }
    }
}
