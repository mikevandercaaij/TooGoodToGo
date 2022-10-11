namespace Core.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Vul een naam voor het pakket in!")]
        public string? Name { get; set; }
        
        public bool ContainsAlcohol { get; set; }

        [Required(ErrorMessage = "Je moet een foto van het product meegeven!")]
        public byte[]? Picture { get; set; }

        public ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}
