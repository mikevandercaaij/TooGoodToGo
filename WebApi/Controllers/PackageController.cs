using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/package")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpPut("{id}/reserve")]
        public async Task<IActionResult> ReservePackage(int id)
        {
            try
            {
                await _packageService.ReservePackageAsync(id, this.User.Identity?.Name!);
                var package = await _packageService.GetPackageByIdAsync(id);

                foreach(Product product in package.Products!)
                {
                    product.Packages = null;
                    product.Picture = null;
                }

                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Het pakket is succesvol gereserveerd.", package });
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = (int)HttpStatusCode.BadRequest, e.Message});
            }
        }
    }
}