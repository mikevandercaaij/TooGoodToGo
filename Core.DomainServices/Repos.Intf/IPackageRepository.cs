namespace Core.DomainServices.Repos.Intf
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task AddPackageAsync(Package package);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(int id);
    }
}