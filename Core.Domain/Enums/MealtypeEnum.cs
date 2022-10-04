using System.ComponentModel;

namespace Core.Domain.Enums
{
    public enum MealtypeEnum
    {
        [Description("Ontbijt")]

        Breakfast,

        [Description("Lunch")]
        Lunch,

        [Description("Avondmaaltijd")]
        Dinner,

        [Description("Warme avondmaaltijd")]
        WarmDinner,

        [Description("Brood")]
        Bread,

        [Description("Drank")]
        Beverage
    }
}