using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sempi5.Controllers
{
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly string frontEndUrl;

        public LoginController(IConfiguration configuration)
        {
            frontEndUrl = configuration["FrontEnd:Url"];
        }
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("LoginResponse", "Login");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult LoginResponse()
        {
            Console.WriteLine("Frontend: " + frontEndUrl);
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var role = claimsIdentity?.FindFirst(ClaimTypes.Role)?.Value;

            if (role != null)
            {
                return role.ToLower() switch
                {
                    "admin" => Redirect(frontEndUrl + "/admin"),
                    "patient" => Redirect(frontEndUrl + "/patient"),
                    _ => Redirect(frontEndUrl + "/staff")
                };
            }
            return Redirect(frontEndUrl + "/errorInvalidRole");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(frontEndUrl);
        }
    }
}
