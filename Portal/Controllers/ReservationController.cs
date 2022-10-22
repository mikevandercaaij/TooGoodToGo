
namespace Portal.Controllers
{
    public class ReservationController : Controller
    {

        private readonly IPackageService _packageService;
        private readonly IStudentService _studentService;

        public ReservationController(IPackageService packageService, IStudentService studentService)
        {
            _packageService = packageService;
            _studentService = studentService;
        }
        

        [HttpGet]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> StudentReservations()
        {
            var reservations = await _packageService.GetAllReservationsFromStudentAsync(this.User.Identity?.Name!);
            return View(reservations);
        }
    }
}