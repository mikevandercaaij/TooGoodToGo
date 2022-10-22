using Core.Domain.Enums;

namespace Portal.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Package> Packages { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}