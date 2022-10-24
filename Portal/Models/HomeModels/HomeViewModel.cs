namespace Portal.Models.HomeModels
{
    public class HomeViewModel
    {
        public IEnumerable<Package> Packages { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}