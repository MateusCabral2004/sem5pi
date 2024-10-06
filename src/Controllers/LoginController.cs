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
        [HttpGet("loginAsPatient")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("LoginResponse", "Login");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("login-response")]
        [Authorize]
        public async Task<IActionResult> LoginResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var claimsIdentity = (ClaimsIdentity)result.Principal.Identity;

            if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Doctor"))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            List<string> roles = claimsIdentity.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Json(new { success = true, email, roles, name });
        }

        [HttpGet("loginAsStaff")]
        public async Task<IActionResult> LocalLogin()
        {
            // Hardcoded user details
            var email = "admin@sempi.pt";
            var name = "admin";
            var role = "Admin";

            // Create claims identity for the hardcoded user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
               // new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect("/Login/login-response");
        }

        [HttpGet("teste1")]
        [Authorize]
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
