namespace Core.Domain.Entities
{
    public class Canteen
    {
        [Key]
        public int CanteenId { get; set; }

        [Required(ErrorMessage = "Geef aan in welke stad de kantine is!")]
        public CityEnum? City { get; set; }

        [Required(ErrorMessage = "Vul een locatie in!")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Geef aan of de kantine warme maaltijden bereid!")]
        public bool? ServesWarmMeals { get; set; }
    }
}
