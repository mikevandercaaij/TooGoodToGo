using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel LoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(LoginModel.UserId);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, LoginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(LoginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            return View(LoginModel);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(StudentRegisterModel studentModel)
        {
            if(!ModelState.IsValid)
            {
                return View(studentModel);
            }

            var user = new IdentityUser
            {
                UserName = studentModel.StudentNumber.ToString(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, studentModel.Password);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Student", "Student"));

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        public IActionResult RegisterCanteenEmployee()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCanteenEmployee(CanteenEmployeeRegisterModel canteenEmployeeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(canteenEmployeeModel);
            }

            var user = new IdentityUser
            {
                UserName = canteenEmployeeModel.EmployeeId.ToString(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, canteenEmployeeModel.Password);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CanteenEmployee", "CanteenEmployee"));

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
