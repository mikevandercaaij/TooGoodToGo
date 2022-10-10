﻿using Core.DomainServices.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Core.DomainServices.Services.Intf;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly IStudentService _studentService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICanteenEmployeeService canteenEmployeeService, IStudentService studentService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _canteenEmployeeService = canteenEmployeeService;
            _studentService = studentService;
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
                    } else
                    {
                        ModelState.AddModelError(nameof(LoginModel.Password), "Het ingevulde wachtwoord is incorrect!");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginModel.Password), "Er bestaat geen gebruiker met deze gegevens!");
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

            var Student = new Student()
            {
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                StudentNumber = studentModel.StudentNumber,
                EmailAddress = studentModel.EmailAddress,
                PhoneNumber = studentModel.PhoneNumber,
                DateOfBirth = studentModel.DateOfBirth,
                StudyCity = studentModel.StudyCity
            };

            await _studentService.AddStudentAsync(Student);

            var result = await _userManager.CreateAsync(user, studentModel.Password);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "Student"));

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

            var CanteenEmployee = new CanteenEmployee()
            {
                FirstName = canteenEmployeeModel.FirstName,
                LastName = canteenEmployeeModel.LastName,
                EmployeeId = canteenEmployeeModel.EmployeeId,
                Location = canteenEmployeeModel.Location
            };

            await _canteenEmployeeService.AddCanteenEmployeeAsync(CanteenEmployee);
            
            var result = await _userManager.CreateAsync(user, canteenEmployeeModel.Password);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "CanteenEmployee"));

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
