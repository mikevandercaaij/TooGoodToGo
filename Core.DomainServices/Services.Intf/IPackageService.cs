using Core.Domain.Enums;

namespace Core.DomainServices.Services.Intf
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task AddPackageAsync(Package package, IList<string> selectedProducts, string userId);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(int id);
        Task ReservePackage(Package package, string studentNumber);
        Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location);

        //Reservations
        Task<IEnumerable<Package>> GetAllReservationsFromStudentAsync(string studentNumber);
        Task<IEnumerable<Package>> GetAllReservationsFromCanteenAsync(CanteenLocationEnum location);


    }
}
