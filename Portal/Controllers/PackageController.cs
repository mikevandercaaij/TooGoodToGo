
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
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IProductService _productService;

        public PackageController(IPackageService packageService, IProductService productService)
        {
            _packageService = packageService;
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var packages = await _packageService.GetAllPackagesAsync();
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
            if(packageModel.SelectedProducts?.Count == 0)
            {
                ModelState.AddModelError(nameof(packageModel.Products), "Selecteer minimaal één product!");
            }

            if (!ModelState.IsValid)
            {
                if (ModelState["Mealtype"]?.Errors.Count > 0)
                {
                    ModelState["Mealtype"]?.Errors.Clear();
                    ModelState["Mealtype"]?.Errors.Add("Selecteer een type maaltijd!");
                }

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
            
            if(package != null)
            {
                ViewBag.Role = this.User.GetRole();
                return View(package);
            }
                
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "CanteenEmployee")]
        public IActionResult OurPackages()
        {
            return View();
        }
    }
}
