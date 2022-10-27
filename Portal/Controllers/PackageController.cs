namespace Portal.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IProductService _productService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;

        public PackageController(IPackageService packageService, IProductService productService, ICanteenEmployeeService canteenEmployeeService)
        {
            _packageService = packageService;
            _productService = productService;
            _canteenEmployeeService = canteenEmployeeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var packages = await _packageService.GetAllOfferedPackagesAsync();
            return View(packages);
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> CreatePackage()
        {
            var model = new PackageModel
            {
                AvailableProducts = await _productService.GetAllSelectListItems()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePackage(PackageModel packageModel)
        {
            try
            {
                var package = new Package()
                {
                    Name = packageModel.Name,
                    PickUpTime = packageModel.PickUpTime,
                    LatestPickUpTime = packageModel.LatestPickUpTime,
                    Price = packageModel.Price,
                    MealType = packageModel.MealType
                };

                await _packageService.AddPackageAsync(package, packageModel.SelectedProducts!, this.User.Identity?.Name!);

                if (ModelState.IsValid)
                    return RedirectToAction("OurPackages", "Package");

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Selecteer minimaal één product!")
                    ModelState.AddModelError("Products", e.Message);

                if (e.Message == "De ophaaltijd moet in de toekomst liggen!")
                    ModelState.AddModelError("PickUpTime", e.Message);

                if (e.Message == "De ophaaltijd mag niet meer dan 2 dagen in de toekomst liggen!")
                    ModelState.AddModelError("PickUpTime", e.Message);

                if (e.Message == "De uiterlijke ophaaltijd moet in de toekomst liggen!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "De uiterlijke ophaaltijd moet na de ophaaltijd plaatsvinden!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "De ophaaltijd en uiterlijke ophaaltijd moeten op dezelfde dag zijn!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "Jouw kantine serveert geen warme maaltijden!")
                    ModelState.AddModelError("MealType", e.Message);

                packageModel.AvailableProducts = await _productService.GetAllSelectListItems();
                return View(packageModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PackageDetails(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if (package != null)
            {
                var model = new PackageDetailsViewModel
                {
                    Package = package,
                };

                if (package.ReservedBy != null)
                    model.Name = package.ReservedBy.FirstName + " " + package.ReservedBy.LastName;


                if (this.User.GetRole() == "CanteenEmployee")
                    model.CanteenEmployeeLocation = (CanteenLocationEnum)_canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity!.Name!).Result?.Location!;

                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> EditPackage(int id)
        {
            try
            {
                await _packageService.ValidateGetUpdatePackage(id, this.User.Identity?.Name!);

                if (ModelState.IsValid)
                    return View(ViewModelHelper.GetPackageViewModel(await _packageService.GetPackageByIdAsync(id), await _productService.GetAllSelectListItems()));

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "De maaltijd is al gereserveerd en mag dan ook niet worden bewerkt!")
                    ModelState.AddModelError("AlreadyResered", e.Message);

                if (e.Message == "Het is niet toegestaan om pakketten van andere kantines te bewerken!")
                    ModelState.AddModelError("NotAllowed", e.Message);

                var package = await _packageService.GetPackageByIdAsync(id);
                return View("PackageDetails", ViewModelHelper.GetPackageDetailsViewModel(package, (CanteenLocationEnum)package.Canteen?.Location!));
            }
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPackage(PackageModel packageModel)
        {
            try
            {
                Package package = (await _packageService.GetPackageByIdAsync((int)packageModel.PackageId!))!;

                package.Name = packageModel.Name;
                package.PickUpTime = packageModel.PickUpTime;
                package.LatestPickUpTime = packageModel.LatestPickUpTime;
                package.Price = packageModel.Price;
                package.MealType = packageModel.MealType;
                package.Products = new List<Product>();

                await _packageService.UpdatePackageAsync(package, packageModel.SelectedProducts!, this.User.Identity?.Name!);
                return RedirectToAction("PackageDetails", new { id = (int)packageModel.PackageId! });
            }
            catch (Exception e)
            {
                if (e.Message == "Selecteer minimaal één product!")
                    ModelState.AddModelError("Products", e.Message);

                if (e.Message == "De ophaaltijd moet in de toekomst liggen!")
                    ModelState.AddModelError("PickUpTime", e.Message);

                if (e.Message == "De ophaaltijd mag niet meer dan 2 dagen in de toekomst liggen!")
                    ModelState.AddModelError("PickUpTime", e.Message);

                if (e.Message == "De uiterlijke ophaaltijd moet in de toekomst liggen!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "De uiterlijke ophaaltijd moet na de ophaaltijd plaatsvinden!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "De ophaaltijd en uiterlijke ophaaltijd moeten op dezelfde dag zijn!")
                    ModelState.AddModelError("LatestPickUpTime", e.Message);

                if (e.Message == "Jouw kantine serveert geen warme maaltijden!")
                    ModelState.AddModelError("MealType", e.Message);

                packageModel.AvailableProducts = await _productService.GetAllSelectListItems();
                return View(packageModel);
            }
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                await _packageService.DeletePackageAsync(id, this.User.Identity?.Name!);

                if (ModelState.IsValid)
                    return RedirectToAction("OurPackages");

                throw new Exception("Er is iets fout gegaan bij het verwijderen van het pakket!");
            }
            catch (Exception e)
            {
                if (e.Message == "De maaltijd is al gereserveerd en mag dan ook niet worden verwijderd!")
                    ModelState.AddModelError("AlreadyReserved", e.Message);

                if (e.Message == "Het is niet toegestaan om pakketten van andere kantines te verwijderen!")
                    ModelState.AddModelError("NotAllowedToAlter", e.Message);

                var package = await _packageService.GetPackageByIdAsync(id);
                return View("PackageDetails", ViewModelHelper.GetPackageDetailsViewModel(package, (CanteenLocationEnum)package.Canteen?.Location!));
            }
        }

        [HttpGet]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> ReservePackage(int id)
        {
            try
            {
                await _packageService.ReservePackageAsync(id, this.User.Identity?.Name!);

                if (ModelState.IsValid)
                {
                    return RedirectToAction("StudentReservations", "Reservation");
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                if (e.Message == "Je hebt al een pakket gereserveerd op deze ophaaldag!")
                    ModelState.AddModelError("AlreadyReservedAPackage", e.Message);

                if (e.Message == "Je bent nog geen 18 jaar oud en mag dus geen pakketten met alcoholische inhoud reserveren!")
                    ModelState.AddModelError("NotAnAdult", e.Message);

                if (e.Message == "Deze maaltijd is al gereserveerd door iemand anders!")
                    ModelState.AddModelError("PackageIsAlreadyReserved", e.Message);

                var package = await _packageService.GetPackageByIdAsync(id);
                return View("PackageDetails", ViewModelHelper.GetPackageDetailsViewModel(package, (CanteenLocationEnum)package.Canteen?.Location!));
            }
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> OurPackages()
        {
            var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);
            var packages = await _packageService.GetAllPackagesFromCanteenAsync((CanteenLocationEnum)user?.Location!);

            return View(packages);
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> OtherPackages()
        {
            var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);
            var packages = await _packageService.GetAllPackagesFromOtherCanteensAsync((CanteenLocationEnum)user?.Location!);

            return View(packages);
        }
    }
}