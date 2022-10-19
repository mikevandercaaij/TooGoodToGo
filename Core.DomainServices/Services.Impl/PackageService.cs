using Core.Domain.Enums;

namespace Core.DomainServices.Services.Impl
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ICanteenRepository _canteenRepository;
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStudentRepository _studentRepository;

        public PackageService(IPackageRepository packageRepository, ICanteenEmployeeRepository canteenEmployeeRepository, ICanteenRepository canteenRepository , IProductRepository productRepository, IStudentRepository studentRepository)
        {
            _packageRepository = packageRepository;
            _canteenEmployeeRepository = canteenEmployeeRepository;
            _canteenRepository = canteenRepository;
            _productRepository = productRepository;
            _studentRepository = studentRepository;
        }

        public async Task AddPackageAsync(Package package, IList<string> selectedProducts, string userId)
        {
            var user = await _canteenEmployeeRepository.GetCanteenEmployeeByIdAsync(userId);
            
            var canteen = await _canteenRepository.GetCanteenByLocationAsync((CanteenLocationEnum)user!.Location!);

            package.Canteen = canteen;

            bool containsAlcohol = false;

            foreach (string productName in selectedProducts)
            {
                var product = await _productRepository.GetProductByNameAsync(productName);
                if(product!.ContainsAlcohol)
                {
                    containsAlcohol = true;
                }
                package.Products.Add(product!);
            }

            package.IsAdult = containsAlcohol;

            foreach (var product in package.Products)
            {
                product.Packages.Add(package);
            }

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
            return allPackages.Where(p => p.Canteen?.Location == location).OrderBy(p => p.PickUpTime);
        }

        public async Task<Package?> GetPackageByIdAsync(int id) => await _packageRepository.GetPackageByIdAsync(id);

        public async Task UpdatePackageAsync(Package package)
        {
            await _packageRepository.UpdatePackageAsync(package);
        }

        public async Task ReservePackage(Package package,Student student)
        {
            if(package.ReservedBy == null)
            {
                package.ReservedBy = student;
                await _packageRepository.UpdatePackageAsync(package);
            }
        }
        public async Task<IEnumerable<Package>> GetAllOfferedPackagesAsync()
        {
            return (await _packageRepository.GetAllPackagesAsync()).Where(p => p.ReservedBy == null).OrderBy(p => p.PickUpTime);
        }

        public async Task<IEnumerable<Package>> GetAllReservationsFromStudentAsync(string studentNumber)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentNumber);
            return (await _packageRepository.GetAllPackagesAsync()).Where(p => p.ReservedBy == student).OrderBy(p => p.PickUpTime);
        }
    }
}
