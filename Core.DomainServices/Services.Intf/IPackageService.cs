using Core.Domain.Enums;

namespace Core.DomainServices.Services.Intf
{
    public interface IPackageService
    {
        //CRUD
        //:::::::::::::::::::::::::::
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package> GetPackageByIdAsync(int id);
        Task<bool> AddPackageAsync(Package package, IList<string> selectedProducts, string userId);
        Task<bool> UpdatePackageAsync(Package package, IList<string> selectedProducts, string userId);
        Task ValidateGetUpdatePackage(int packageId, string userId);
        Task<bool> DeletePackageAsync(int packageId, string userId);

        //Reservations
        //:::::::::::::::::::::::::::
        Task<bool> ReservePackageAsync(int packageId, string userId);
        Task<IEnumerable<Package>> GetAllReservationsFromStudentAsync(string studentNumber);
        Task<IEnumerable<Package>> GetAllActiveReservationsFromStudentAsync(string studentNumber);

        //Custom
        //:::::::::::::::::::::::::::
        Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location);
        Task<IEnumerable<Package>> GetAllActivePackagesFromCanteenAsync(CanteenLocationEnum location);
        Task<IEnumerable<Package>> GetAllOfferedPackagesAsync();
        Task<bool> IsOurCanteensPackageAsync(Package package, string userId);
        Task<Package> ValidatePackageFormInput(Package package, IList<string> selectedProducts, string userId);
    }
}