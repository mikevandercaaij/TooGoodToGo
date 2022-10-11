using Core.Domain.Enums;

namespace Core.DomainServices.Services.Impl
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task AddPackageAsync(Package package)
        {
            await _packageRepository.AddPackageAsync(package);
        }
        
        public async Task DeletePackageAsync(int id)
        {
            await _packageRepository.DeletePackageAsync(id);
        }

        public async Task<IEnumerable<Package>> GetAllPackagesAsync() => await _packageRepository.GetAllPackagesAsync();

        public async Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location)
        {
            var allPackages = await _packageRepository.GetAllPackagesAsync();
            return allPackages.Where(p => p.Canteen?.Location == location);
        }

        public async Task<Package?> GetPackageByIdAsync(int id) => await _packageRepository.GetPackageByIdAsync(id);

        public async Task UpdatePackageAsync(Package package)
        {
            await _packageRepository.UpdatePackageAsync(package);
        }
    }
}
