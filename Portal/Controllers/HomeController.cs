
using Core.Domain.Entities;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPackageService _packageService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly IStudentService _studentService;

        public HomeController(IPackageService packageService, ICanteenEmployeeService canteenEmployeeService, IStudentService studentService)
        {
            _packageService = packageService;
            _canteenEmployeeService = canteenEmployeeService;
            _studentService = studentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var role = this.User.GetRole();

            if (role == "Student")
            {
                var student = await _studentService.GetStudentByIdAsync(this.User.Identity?.Name!);
                var reservations = await _packageService.GetAllActiveReservationsFromStudentAsync(this.User.Identity?.Name!);

                var model = new HomeViewModel
                {
                    Name = student?.FirstName!,
                    Packages = reservations
                };

                return View(model);
            }

            else if (role == "CanteenEmployee")
            {
                var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByIdAsync(this.User.Identity?.Name!);
                var reservations = await _packageService.GetAllActivePackagesFromCanteenAsync((CanteenLocationEnum)canteenEmployee?.Location!);

                var model = new HomeViewModel
                {
                    Name = canteenEmployee?.FirstName!,
                    Packages = reservations
                };

                return View(model);
            }
            else
            {
                return View("Index");
            }
        }
    }
}