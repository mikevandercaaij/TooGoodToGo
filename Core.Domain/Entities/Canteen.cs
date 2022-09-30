namespace Core.Domain.Entities
{
    public class Canteen
    {
        [Key]
        private int CanteenId { get; set; }

        [Required(ErrorMessage = "Geef aan in welke stad de kantine is!")]
        private CityEnum? City { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        private string? Location { get; set; }

        [Required(ErrorMessage = "Geef aan of de kantine warme maaltijden bereid!")]
        private bool? ServesWarmMeals { get; set; }
    }
}
