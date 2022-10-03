namespace Core.DomainServices
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package> GetPackageByIdAsync(int id);
        Task AddPackageAsync(Package package);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(Package package);
    }
}