using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace WebApi.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("api/signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationCredentials authenticationCredentials)
        {
            var user = await _userManager.FindByNameAsync(authenticationCredentials.Identifier);
            if (user != null)
            {
                if ((await _signInManager.PasswordSignInAsync(user, authenticationCredentials.Password, false, false)).Succeeded)
                {
                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMinutes"])),

                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"])), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

                    return Ok(new { Succes = true, Token = handler.WriteToken(securityToken) });
                }
            }
            return BadRequest();
        }

        [HttpPost("api/signout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
