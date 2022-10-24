namespace Portal.Models.PackageModels
{
    public class PackageDetailsViewModel
    {
        public Package? Package { get; set; }
        public CanteenLocationEnum CanteenEmployeeLocation { get; set; }
        public string? Name { get; set; }
    }
}