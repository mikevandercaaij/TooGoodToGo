using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.ExtensionMethods;
using Portal.Models;
using System.Collections.Generic;
using System.Diagnostics;

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

        [HttpGet]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> ReservationDetails(int id)
        {
            ViewBag.User = await _studentService.GetStudentByIdAsync(this.User.Identity?.Name!);
            var reservation = await _packageService.GetPackageByIdAsync(id);
            return View(reservation);
        }
    }
}