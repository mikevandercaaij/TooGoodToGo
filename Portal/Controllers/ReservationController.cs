
namespace Portal.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IPackageService _packageService;

        public ReservationController(IPackageService packageService)
        {
            _packageService = packageService;
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