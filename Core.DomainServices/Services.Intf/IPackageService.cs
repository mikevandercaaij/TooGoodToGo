using Core.Domain.Enums;

namespace Core.DomainServices.Services.Intf
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task AddPackageAsync(Package package);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(int id);
        Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location);

    }
}
