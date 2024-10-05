using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sempi5.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Login");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        [Authorize]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var claimsIdentity = (ClaimsIdentity)result.Principal.Identity;

            if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Doctor"))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Doctor"));
            }

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            List<string> roles = claimsIdentity.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Json(new { success = true, email, roles, name });
        }

        [HttpGet("teste1")]
        [Authorize(Roles = "Teste")]
        public IActionResult Teste1()
        {
            return Json(new { success = true, message = "Teste1" });
        }

        [HttpGet("teste2")]
        [Authorize(Roles = "Teste2")]
        public IActionResult Teste2()
        {
            return Json(new { success = true, message = "Teste2" });
        }

        [HttpGet("teste3")]
        [Authorize(Policy = "Staff")]
        public IActionResult Teste3()
        {
            return Json(new { success = true, message = "Teste3" });
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);            
            return Redirect("/Login");
        }

    }
}
