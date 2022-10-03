namespace Core.Domain.Entities
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "Vul een naam voor het pakket in!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Kies één of meerdere producten die het pakket bevat!")]
        public ICollection<Product>? Products { get; set; }

        [Required(ErrorMessage = "Geef aan van welke kantine dit pakket is!")]
        public Canteen? Canteen { get; set; }

        [Required(ErrorMessage = "Geef aan wanneer het pakket opgehaald moet worden!")]
        public DateTime? PickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan tot wanneer het pakket opgehaald mag worden!")]
        public DateTime? LatestPickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan of het pakket voor volwassenen is bedoeld!")]
        public bool? IsAdult { get; set; }

        [Required(ErrorMessage = "Geef aan hoe duur het pakket moet worden!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Geef het type pakket aan!")]
        public MealtypeEnum? MealType { get; set; }

        public Student? ReservedBy { get; set; }
    }
}