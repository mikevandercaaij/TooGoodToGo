
namespace Portal.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPackageService _packageService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;

        public HomeController(IPackageService packageService, ICanteenEmployeeService canteenEmployeeService)
        {
            _packageService = packageService;
            _canteenEmployeeService = canteenEmployeeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var role = this.User.GetRole();

            if (role == "Student")
            {
                var reservations = await _packageService.GetAllActiveReservationsFromStudentAsync(this.User.Identity?.Name!);
                return View(reservations);
            }
            else if (role == "CanteenEmployee")
            {
                var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);
                var reservations = await _packageService.GetAllActivePackagesFromCanteenAsync((CanteenLocationEnum)canteenEmployee?.Location!);
                return View(reservations);
            }
            else
            {
                return View("Index");
            }
        }
    }
}