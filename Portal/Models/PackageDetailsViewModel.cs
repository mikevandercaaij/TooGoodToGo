using Core.Domain.Enums;

namespace Portal.Models
{
    public class PackageDetailsViewModel
    {
        public Package? Package { get; set; }
        public CanteenLocationEnum? CanteenEmployeeLocation { get; set; }

    }
}