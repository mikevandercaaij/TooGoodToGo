using System.ComponentModel;

namespace Core.Domain.Enums
{
    public enum MealtypeEnum
    {
        [Display(Name = "Ontbijt")]

        Breakfast = 1,

        [Display(Name = "Lunch")]
        Lunch,

        [Display(Name = "Avondmaaltijd")]
        Dinner,

        [Display(Name = "Warme avondmaaltijd")]

        WarmDinner,
         
        [Display(Name = "Brood")]
        Bread,

        [Display(Name = "Drank")]
        Beverage
    }
}