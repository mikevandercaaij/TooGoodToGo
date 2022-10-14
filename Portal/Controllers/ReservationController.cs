
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.DomainServices.Services.Impl;
using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.ExtensionMethods;
using Portal.Models;
using System.ComponentModel.DataAnnotations;

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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var packages = await _packageService.GetAllPackagesAsync();
            return View(packages);
        }
    }
}
