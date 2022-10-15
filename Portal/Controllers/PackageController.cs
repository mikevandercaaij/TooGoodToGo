
namespace Portal.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IProductService _productService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly IStudentService _studentService;

        public PackageController(IPackageService packageService, IProductService productService, ICanteenEmployeeService canteenEmployeeService, IStudentService studentService)
        {
            _packageService = packageService;
            _productService = productService;
            _canteenEmployeeService = canteenEmployeeService;
            _studentService = studentService;
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
        public async Task<IActionResult> CreatePackage(PackageModel packageModel)
        {
            if(packageModel.SelectedProducts?.Count == 0)
            {
                ModelState.AddModelError(nameof(packageModel.Products), "Selecteer minimaal één product!");
            }

            if (!ModelState.IsValid)
            {
                if (ModelState["Mealtype"]?.Errors.Count > 0)
                {
                    ModelState["Mealtype"]?.Errors.Clear();
                    ModelState["Mealtype"]?.Errors.Add("Selecteer een type maaltijd!");
                }

                packageModel.AvailableProducts = await _productService.GetAllSelectListItems();
                return View(packageModel);
            }

            var package = new Package()
            {
                Name = packageModel.Name,
                PickUpTime = packageModel.PickUpTime,
                LatestPickUpTime = packageModel.LatestPickUpTime,
                Price = packageModel.Price,
                MealType = packageModel.MealType,
            };

            await _packageService.AddPackageAsync(package, packageModel.SelectedProducts!, this.User.Identity!.Name!);
            return RedirectToAction("OurPackages", "Package");
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

                if (this.User.GetRole() == "CanteenEmployee")
                {
                    model.CanteenEmployeeLocation = _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity!.Name!).Result?.Location;
                }
                
                return View(model);
            }
                
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> OurPackages()
        {
            var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);
            var packages = await _packageService.GetAllPackagesFromCanteenAsync((CanteenLocationEnum)user?.Location!);
            return View(packages);
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if(package?.ReservedBy == null)
            {
                await _packageService.DeletePackageAsync(id);
                return RedirectToAction("OurPackages");
            }
            return View("Index", "Home");
        }

        [Authorize(Policy = "Student")]
        public async Task<IActionResult> ReservePackage(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if (package != null && package?.ReservedBy == null)
            {
                var student = await _studentService.GetStudentByIdAsync(this.User.Identity?.Name!);
                
                if (student != null)
                {
                    await _packageService.ReservePackage(package!, student);
                    return RedirectToAction("StudentReservations", "Reservation");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}