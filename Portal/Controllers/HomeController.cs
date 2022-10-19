
namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var role = this.User.GetRole();

            if (role == "Student")
            {
                return View("Index", "Student");
            }
            else if (role == "CanteenEmployee")
            {
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }
    }
}