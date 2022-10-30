namespace Core.DomainServices.Services.Impl
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ICanteenService _canteenService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly IProductService _productService;
        private readonly IStudentService _studentService;
        public PackageService(IPackageRepository packageRepository, ICanteenEmployeeService canteenEmployeeService, ICanteenService canteenService, IProductService productService, IStudentService studentService)
        {
            _packageRepository = packageRepository;
            _canteenService = canteenService;
            _canteenEmployeeService = canteenEmployeeService;
            _productService = productService;
            _studentService = studentService;
        }

        //CRUD
        //:::::::::::::::::::::::::::

        //Get all packages
        public async Task<IEnumerable<Package>> GetAllPackagesAsync() => await _packageRepository.GetAllPackagesAsync();

        //Get specific package
        public async Task<Package> GetPackageByIdAsync(int id) => await _packageRepository.GetPackageByIdAsync(id);

        //Create package
        public async Task<bool> AddPackageAsync(Package package, IList<string> selectedProducts, string userId)
        {
            var alteredPackage = await ValidatePackageFormInput(package, selectedProducts, userId);
            alteredPackage.Canteen = await _canteenService.GetCanteenByLocationAsync((CanteenLocationEnum)_canteenEmployeeService.GetCanteenEmployeeByIdAsync(userId).Result!.Location!);
            await _packageRepository.AddPackageAsync(package);

            return true;
        }

        //Update package
        public async Task<bool> UpdatePackageAsync(Package package, IList<string> selectedProducts, string userId)
        {
            await _packageRepository.UpdatePackageAsync(await ValidatePackageFormInput(package, selectedProducts, userId));
            return true;
        }

        //Validation to get package update page
        public async Task ValidateGetUpdatePackage(int packageId, string userId)
        {
            var package = await GetPackageByIdAsync(packageId);

            if (package != null)
            {
                bool isOurPackage = await IsOurCanteensPackageAsync(package, userId);

                if (isOurPackage)
                {
                    if (package.ReservedBy != null)
                        throw new Exception("De maaltijd is al gereserveerd en mag dan ook niet worden bewerkt!");
                    return;
                }
                else
                    throw new Exception("Het is niet toegestaan om pakketten van andere kantines te bewerken!");
            }
            throw new Exception("Het pakket bestaat niet!");
        }

        //Delete package
        public async Task<bool> DeletePackageAsync(int packageId, string userId)
        {
            var package = await GetPackageByIdAsync(packageId);

            if (package != null)
                if (await IsOurCanteensPackageAsync(package, userId))
                    if (package.ReservedBy == null || DateTime.Now > package.LatestPickUpTime)
                    {
                        await _packageRepository.DeletePackageAsync(package.PackageId);
                        return true;
                    }
                    else
                        throw new Exception("De maaltijd is al gereserveerd en mag dan ook niet worden verwijderd!");
                else
                    throw new Exception("Het is niet toegestaan om pakketten van andere kantines te verwijderen!");
            else
                throw new Exception("Het pakket bestaat niet!");
        }

        //Reservations
        //:::::::::::::::::::::::::::

        //Reserve package
        public async Task<bool> ReservePackageAsync(int packageId, string userId)
        {
            var package = await GetPackageByIdAsync(packageId);

            if (package != null && package?.ReservedBy == null)
            {
                var student = await _studentService.GetStudentByIdAsync(userId);

                if (student != null)
                {
                    var studentIsAdult = student.DateOfBirth!.Value.AddYears(18) <= package!.PickUpTime!.Value;

                    if (package!.IsAdult!.Value && studentIsAdult || !(package!.IsAdult!.Value))
                    {
                        var packages = await GetAllReservationsFromStudentAsync(userId);

                        bool alreadyReserved = false;

                        foreach (Package p in packages)
                        {
                            if (p.PickUpTime!.Value.Date == package.PickUpTime.Value.Date)
                                alreadyReserved = true;
                        }

                        if (!alreadyReserved)
                        {
                            package.ReservedBy = student;
                            await _packageRepository.UpdatePackageAsync(package);
                            return true;
                        }
                        else
                            throw new Exception("Je hebt al een pakket gereserveerd op deze ophaaldag!");
                    }
                    else
                        throw new Exception("Je bent nog geen 18 jaar oud en mag dus geen pakketten met alcoholische inhoud reserveren!");
                }
                else
                    throw new Exception("Je account is niet geldig!");
            }
            else
                throw new Exception("Het pakket bestaat niet of is al gereserveerd door iemand anders!");
        }
        //Get all reservations from students
        public async Task<IEnumerable<Package>> GetAllReservationsFromStudentAsync(string studentNumber)
        {
            var student = await _studentService.GetStudentByIdAsync(studentNumber);
            return (await _packageRepository.GetAllPackagesAsync()).Where(p => p.ReservedBy == student).OrderBy(p => p.PickUpTime);
        }

        //Get all active reservations from students
        public async Task<IEnumerable<Package>> GetAllActiveReservationsFromStudentAsync(string studentNumber)
        {
            var student = await _studentService.GetStudentByIdAsync(studentNumber);
            return (await _packageRepository.GetAllPackagesAsync()).Where(p => p.ReservedBy == student).Where(p => p.LatestPickUpTime > DateTime.Now).OrderBy(p => p.PickUpTime);
        }

        //Custom
        //:::::::::::::::::::::::::::

        //Get all packages from canteen
        public async Task<IEnumerable<Package>> GetAllPackagesFromCanteenAsync(CanteenLocationEnum location)
        {
            var allPackages = await _packageRepository.GetAllPackagesAsync();
            return allPackages.Where(p => p.Canteen?.Location == location && p.LatestPickUpTime > DateTime.Now).OrderBy(p => p.PickUpTime);
        }

        //Get all active packages from canteen
        public async Task<IEnumerable<Package>> GetAllActivePackagesFromCanteenAsync(CanteenLocationEnum location)
        {
            var allPackages = await _packageRepository.GetAllPackagesAsync();
            return allPackages.Where(p => p.Canteen?.Location == location).Where(p => p.LatestPickUpTime > DateTime.Now).Where(p => p.ReservedBy != null).OrderBy(p => p.PickUpTime);
        }

        //Get all offered packages
        public async Task<IEnumerable<Package>> GetAllOfferedPackagesAsync()
        {
            return (await _packageRepository.GetAllPackagesAsync()).Where(p => p.ReservedBy == null).Where(p => p.LatestPickUpTime > DateTime.Now).OrderBy(p => p.PickUpTime);
        }

        //Get all packages from other canteens
        public async Task<IEnumerable<Package>> GetAllPackagesFromOtherCanteensAsync(CanteenLocationEnum location)
        {
            var allPackages = await _packageRepository.GetAllPackagesAsync();
            return allPackages.Where(p => p.Canteen?.Location != location && p.LatestPickUpTime > DateTime.Now).OrderBy(p => p.PickUpTime);
        }


        //Check if package is from user's canteen
        public async Task<bool> IsOurCanteensPackageAsync(Package package, string userId)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(userId);

            if (canteenEmployee != null)
                if (canteenEmployee.Location == package.Canteen!.Location)
                    return true;
            return false;
        }

        //Validate form input package
        public async Task<Package> ValidatePackageFormInput(Package package, IList<string> selectedProducts, string userId)
        {
            var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(userId);
            var canteen = await _canteenService.GetCanteenByLocationAsync(user!.Location!.Value);

            if (selectedProducts?.Count == 0)
                throw new Exception("Selecteer minimaal één product!");

            if (package.PickUpTime < DateTime.Now)
                throw new Exception("De ophaaltijd moet in de toekomst liggen!");

            if (package.PickUpTime.HasValue)
                if (package.PickUpTime.Value.Day > DateTime.Now.AddDays(2).Day || package.PickUpTime.Value.Month != DateTime.Now.AddDays(2).Month || package.PickUpTime.Value.Year != DateTime.Now.AddDays(2).Year)
                    throw new Exception("De ophaaltijd mag niet meer dan 2 dagen in de toekomst liggen!");
                else if (package.LatestPickUpTime.HasValue)
                {
                    if (package.LatestPickUpTime < DateTime.Now)
                        throw new Exception("De uiterlijke ophaaltijd moet in de toekomst liggen!");

                    else if (package.LatestPickUpTime <= package.PickUpTime)
                        throw new Exception("De uiterlijke ophaaltijd moet na de ophaaltijd plaatsvinden!");

                    else if (package.LatestPickUpTime.Value.Day != package.PickUpTime.Value.Day || package.LatestPickUpTime.Value.Month != package.PickUpTime.Value.Month || package.LatestPickUpTime.Value.Year != package.PickUpTime.Value.Year)
                        throw new Exception("De ophaaltijd en uiterlijke ophaaltijd moeten op dezelfde dag zijn!");
                }

            if (!canteen!.ServesWarmMeals!.Value && package.MealType == MealtypeEnum.WarmDinner)
                throw new Exception("Jouw kantine serveert geen warme maaltijden!");

            bool containsAlcohol = false;

            foreach (string productName in selectedProducts!)
            {
                var product = await _productService.GetProductByNameAsync(productName);
                if (product!.ContainsAlcohol)
                {
                    containsAlcohol = true;
                }
                package.Products.Add(product!);
                product?.Packages!.Add(package);
            }

            package.IsAdult = containsAlcohol;

            return package;
        }
    }
}
