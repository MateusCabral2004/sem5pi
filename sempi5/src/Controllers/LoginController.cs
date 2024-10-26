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
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("LoginResponse", "Login");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> LoginResponse()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var role = claimsIdentity?.FindFirst(ClaimTypes.Role).Value;
            if (role.Equals("Unregistered"))
            {
                return Redirect("patient/register");
            }
            if(role.Equals("Unverified"))
            {
                return Ok("Please verify your account using the link sent to your email and then login again");
            }
            
            if (role.Equals("Patient"))
            {
                //implement this in patient controller and if user exist show profile else show registration form
                return Redirect("Patient/Home");
            }

            if (role.Equals("Admin"))
            {
                return Redirect("Admin/Home");
            }

            //implement this in staff controller
            return Redirect("Staff/Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Redirect("/Login/login");
        }

    }
}
