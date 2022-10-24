using System;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Portal.ExtensionMethods
{
    public static class PackageExtensions
    {
        public static string DisplayProducts(this Package package)
        {
            var sb = new StringBuilder();

            var amountOfProducts = package.Products.Count;


            for (int i = 0; i < amountOfProducts; i++)
            {
                sb.Append(package.Products.ToArray()[i].Name);
                if (i != amountOfProducts - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        public static string GetLatestPickupTime(this Package package) => package.LatestPickUpTime!.Value.ToString("HH:mm");
        public static string GetLatestPickupDate(this Package package) => package.LatestPickUpTime!.Value.ToString("dd-M-yyyy");
        public static string GetPickupDate(this Package package) => package.PickUpTime!.Value.ToString("dd-M-yyyy");
        public static string GetPickupTime(this Package package) => package.PickUpTime!.Value.ToString("HH:mm");

        public static string GetPickupDateAndTime(this Package package) => package.PickUpTime!.Value.ToString("dd-M-yyyy HH:mm");
        public static string GetPrice(this Package package) => String.Format("{0:€#,##0.00}", package.Price);
        public static string ContainsAlcohol(this Package package) => package.IsAdult!.Value ? "<i class='fa-solid fa-check --custom-check'></i>" : "<i class='fa-solid fa-xmark --custom-x'></i>";
        public static string IsReserved(this Package package) => package.ReservedBy != null ? "<i class='fa-solid fa-check --custom-check'></i>" : "<i class='fa-solid fa-xmark --custom-x'></i>";
        
        public static string GetMealtypeName(this Package package)
        {
            switch(package.MealType)
            {
                case MealtypeEnum.Breakfast:
                    return "Ontbijt";
                case MealtypeEnum.Lunch:
                    return "Lunch";
                case MealtypeEnum.WarmDinner:
                    return "Warme avondmaaltijd";
                case MealtypeEnum.Bread:
                    return "Broodmaaltijd";
                case MealtypeEnum.Beverage:
                    return "Drank";
                default: return "Ongeldig Maaltijdtype";
            }
        }
    }

}
