using Core.Domain.Enums;

namespace Core.DomainServices.Services.Intf
{
    public interface IPackageService
    {
        //CRUD
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task AddPackageAsync(Package package, IList<string> selectedProducts, string userId);
        Task UpdatePackageAsync(Package package, IList<string> selectedProducts);
        Task DeletePackageAsync(int id);

        //Custom
        Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location);
        Task<IEnumerable<Package>> GetAllActivePackagesFromCanteenAsync(CanteenLocationEnum location);
        Task<IEnumerable<Package>> GetAllOfferedPackagesAsync();

        //Reservations
        Task ReservePackage(Package package, Student student);
        Task<IEnumerable<Package>> GetAllReservationsFromStudentAsync(string studentNumber);
        Task<IEnumerable<Package>> GetAllActiveReservationsFromStudentAsync(string studentNumber);
    }
}
