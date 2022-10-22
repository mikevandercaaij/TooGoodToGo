
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portal.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Portal.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IProductService _productService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly ICanteenService _canteenService;
        private readonly IStudentService _studentService;

        public PackageController(IPackageService packageService, IProductService productService, ICanteenEmployeeService canteenEmployeeService, IStudentService studentService, ICanteenService canteenService)
        {
            _packageService = packageService;
            _productService = productService;
            _canteenEmployeeService = canteenEmployeeService;
            _studentService = studentService;
            _canteenService = canteenService;
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
            if (ModelState["Mealtype"]?.Errors.Count > 0)
            {
                ModelState["Mealtype"]?.Errors.Clear();
                ModelState["Mealtype"]?.Errors.Add("Selecteer een type maaltijd!");
            }

            if (!(ModelState["Mealtype"]?.Errors.Count > 0))
            {
                var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);

                if (user != null)
                {
                    var canteen = await _canteenService.GetCanteenByLocationAsync(user.Location!.Value);

                    if (canteen != null)
                    {
                        if (!canteen.ServesWarmMeals!.Value && packageModel.MealType == MealtypeEnum.WarmDinner)
                        {
                            ModelState["Mealtype"]?.Errors.Clear();
                            ModelState["Mealtype"]?.Errors.Add("Jouw kantine serveert geen warme maaltijden!");
                        }
                    }
                }
            }

            if (packageModel.PickUpTime < DateTime.Now)
            {
                ModelState.AddModelError("PickUpTime", "De afhaaltijd moet in de toekomst liggen!");
            }

            if (packageModel.PickUpTime.HasValue)
            {
                if (packageModel.PickUpTime.Value.Day > DateTime.Now.AddDays(2).Day)
                {
                    ModelState.AddModelError("PickUpTime", "De afhaaltijd mag niet meer dan 2 dagen in de toekomst liggen!");
                }
            }

            if (packageModel.LatestPickUpTime < DateTime.Now)
            {
                ModelState.AddModelError("LatestPickUpTime", "De uiterlijke afhaaltijd moet in de toekomst liggen!");
            }
            else if (packageModel.LatestPickUpTime <= packageModel.PickUpTime)
            {
                ModelState.AddModelError("LatestPickUpTime", "De uiterlijke afhaaltijd moet na de afhaaltijd plaatsvinden!");
            }

            if (packageModel.SelectedProducts?.Count == 0)
            {
                ModelState.AddModelError("Products", "Selecter minimaal één product!");
            }


            if (!ModelState.IsValid)
            {
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

                if(package.ReservedBy != null)
                {
                    model.Name = package.ReservedBy.FirstName + " " + package.ReservedBy.LastName;
                }

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

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> EditPackage(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if (package != null)
            {
                bool isOurPackage = await IsOurCanteensPackage(package);

                if (isOurPackage)
                {
                    if (package.ReservedBy == null || DateTime.Now > package.LatestPickUpTime)
                    {
                        var module = new PackageModel()
                        {
                            PackageId = package!.PackageId,
                            Name = package!.Name,
                            Products = package!.Products,
                            PickUpTime = package!.PickUpTime,
                            LatestPickUpTime = package!.LatestPickUpTime,
                            Price = package!.Price,
                            MealType = package!.MealType,
                            AvailableProducts = await _productService.GetAllSelectListItems(),
                        };

                        var _selectedProducts = new List<string>();

                        foreach (Product p in package.Products)
                        {
                            _selectedProducts.Add(p.Name!);
                        }

                        module.SelectedProducts = _selectedProducts;

                        return View(module);
                    }
                    else
                    {
                        ModelState.AddModelError("EditPackage", "De maaltijd is al gereserveerd en mag dan ook niet worden bewerkt!");
                    }
                }
                else
                {
                    ModelState.AddModelError("EditPackage", "Het is niet toegestaan om pakketten van andere kantines te bewerken!");
                }
            }

            var model = new PackageDetailsViewModel
            {
                Package = package,
            };

            return View("PackageDetails", model);
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> EditPackage(PackageModel packageModel)
        {
            if (ModelState["Mealtype"]?.Errors.Count > 0)
            {
                ModelState["Mealtype"]?.Errors.Clear();
                ModelState["Mealtype"]?.Errors.Add("Selecteer een type maaltijd!");
            }

            if (!(ModelState["Mealtype"]?.Errors.Count > 0))
            {
                var user = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);

                if (user != null)
                {
                    var canteen = await _canteenService.GetCanteenByLocationAsync(user.Location!.Value);

                    if (canteen != null)
                    {
                        if (!canteen.ServesWarmMeals!.Value && packageModel.MealType == MealtypeEnum.WarmDinner)
                        {
                            ModelState["Mealtype"]?.Errors.Clear();
                            ModelState["Mealtype"]?.Errors.Add("Jouw kantine serveert geen warme maaltijden!");
                        }
                    }
                }
            }

            if (packageModel.PickUpTime < DateTime.Now)
            {
                ModelState.AddModelError("PickUpTime", "De afhaaltijd moet in de toekomst liggen!");
            }

            if (packageModel.PickUpTime.HasValue)
            {
                if (packageModel.PickUpTime.Value.Day > DateTime.Now.AddDays(2).Day)
                {
                    ModelState.AddModelError("PickUpTime", "De afhaaltijd mag niet meer dan 2 dagen in de toekomst liggen!");
                }
            }

            if (packageModel.LatestPickUpTime < DateTime.Now)
            {
                ModelState.AddModelError("LatestPickUpTime", "De uiterlijke afhaaltijd moet in de toekomst liggen!");
            }
            else if (packageModel.LatestPickUpTime <= packageModel.PickUpTime)
            {
                ModelState.AddModelError("LatestPickUpTime", "De uiterlijke afhaaltijd moet na de afhaaltijd plaatsvinden!");
            }

            if (packageModel.SelectedProducts?.Count == 0)
            {
                ModelState.AddModelError("Products", "Selecter minimaal één product!");
            }

            if (!ModelState.IsValid)
            {
                packageModel.AvailableProducts = await _productService.GetAllSelectListItems();
                return View(packageModel);
            }


            Package package = (await _packageService.GetPackageByIdAsync((int)packageModel.PackageId!))!;

            package.Name = packageModel.Name;
            package.PickUpTime = packageModel.PickUpTime;
            package.LatestPickUpTime = packageModel.LatestPickUpTime;
            package.Price = packageModel.Price;
            package.MealType = packageModel.MealType;
            package.Products = new List<Product>();

            await _packageService.UpdatePackageAsync(package, packageModel.SelectedProducts!);
            return RedirectToAction("PackageDetails", new { id = (int)packageModel.PackageId! });
        }

        [HttpPost]
        [Authorize(Policy = "CanteenEmployee")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if (package != null)
            {
                bool isOurPackage = await IsOurCanteensPackage(package);

                if (isOurPackage)
                {
                    if (package.ReservedBy == null || DateTime.Now > package.LatestPickUpTime)
                    {
                        await _packageService.DeletePackageAsync(id);
                        return RedirectToAction("OurPackages");
                    }
                    else
                    {
                        ModelState.AddModelError("DeletePackage", "De maaltijd is al gereserveerd en mag dan ook niet worden verwijderd!");
                    }
                }
                else
                {
                    ModelState.AddModelError("DeletePackage", "Het is niet toegestaan om pakketten van andere kantines te verwijderen!");
                }
            }

            var model = new PackageDetailsViewModel
            {
                Package = package,
            };

            return View("PackageDetails", model);
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
                    var studentIsAdult = student.DateOfBirth!.Value.AddYears(18) <= package!.PickUpTime!.Value;

                    if (package!.IsAdult!.Value && studentIsAdult || !(package!.IsAdult!.Value))
                    {

                        var packages = await _packageService.GetAllReservationsFromStudentAsync(this.User.Identity?.Name!);

                        bool alreadyReserved = false;

                        foreach (Package p in packages)
                        {
                            if (p.PickUpTime!.Value.Date == package.PickUpTime.Value.Date)
                            {
                                alreadyReserved = true;
                            }
                        }

                        if (!alreadyReserved)
                        {
                            await _packageService.ReservePackage(package!, student);
                            return RedirectToAction("StudentReservations", "Reservation");
                        }
                        else
                        {
                            ModelState.AddModelError("Reservation", "Je hebt al een pakket gereserveerd op deze ophaaldag!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Reservation", "Je bent nog geen 18 jaar oud en mag dus geen pakketten met alcoholische inhoud reserveren!");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Reservation", "Deze maaltijd is al gereserveerd door iemand anders!");
            }


            var model = new PackageDetailsViewModel
            {
                Package = package,
            };

            return View("PackageDetails", model);
        }

        public async Task<bool> IsOurCanteensPackage(Package package)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);

            if (canteenEmployee != null)
            {
                if (canteenEmployee.Location == package.Canteen!.Location)
                {
                    return true;
                }
            }
            return false;
        }
    }
}