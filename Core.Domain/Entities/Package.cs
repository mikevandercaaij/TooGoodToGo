namespace Core.Domain.Entities
{
    public class Package
    {
        [Key]
        private int PackageId { get; set; }

        [Required(ErrorMessage = "Vul een naam voor het pakket in!")]
        private string? Name { get; set; }

        [Required(ErrorMessage = "Kies één of meerdere producten die het pakket bevat!")]
        private ICollection<Product>? Products { get; set; }

        [Required(ErrorMessage = "Geef aan van welke kantine dit pakket is!")]
        private Canteen? Canteen { get; set; }

        [Required(ErrorMessage = "Geef aan wanneer het pakket opgehaald moet worden!")]
        private DateTime? PickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan tot wanneer het pakket opgehaald mag worden!")]
        private TimeOnly? LatestPickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan of het pakket voor volwassenen is bedoeld!")]
        private bool? IsAdult { get; set; }

        [Required(ErrorMessage = "Geef aan hoe duur het pakket moet worden!")]
        private decimal? Price { get; set; }

        [Required(ErrorMessage = "Geef het type pakket aan!")]
        private MealtypeEnum? MealType { get; set; }

        private Student? ReservedBy { get; set; }
    }
}