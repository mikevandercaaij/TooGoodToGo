using System.Security.Claims;

namespace Portal.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static string GetGreetingMessage(this DateTime dateTime)
        {
            return dateTime.Hour switch
            {
                var hour when hour >= 0 && hour < 6 => "Goedenacht",
                var hour when hour >= 6 && hour < 12 => "Goedemorgen",
                var hour when hour >= 12 && hour < 18 => "Goedemiddag",
                var hour when hour >= 18 && hour < 24 => "Goedenavond",
                _ => "Hallo",
            };
        }
    }
}
    