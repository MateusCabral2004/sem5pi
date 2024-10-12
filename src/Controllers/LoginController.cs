using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sempi5.Domain.Staff;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;
using Sempi5.Services;

namespace Sempi5.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {

        private readonly LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }


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
            var result = await HttpContext.AuthenticateAsync("LocalCookie");
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var claimsIdentity = (ClaimsIdentity)result.Principal.Identity;

            string role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            if(!role.IsNullOrEmpty())
            {
               return Json(new { success = true, email, role, name }); 
            }

            if (await loginService.getPatientFromEmail(email) != null)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Patient"));
            }
            else
            {
                var staff = await loginService.getStaffFromEmail(email);

                if (staff != null)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, staff.User.Role));
                } else
                {
                    return Json("No account found Its needed to create" +
                    "a method that redirects to the register page for patients");
                }
            }

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync("LocalCookie", new ClaimsPrincipal(claimsIdentity), authProperties);

            List<string> roles = claimsIdentity.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Json(new { success = true, email, roles, name });
        }

        [HttpPost("loginAsStaff")]
        public async Task<IActionResult> LocalLogin([FromBody] LoginDTO dto)
        {

            if (dto == null)
            {
                return BadRequest("NULL DTO");
            }

            Console.WriteLine("teste");

            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid email or password");
            }

            Staff staff = await loginService.getStaffFromEmail(dto.Email);

            if (staff.User == null || staff == null)
            {
                return BadRequest("Invalid email or password");
            }

            if (staff.Password != dto.Password)
            {
                return BadRequest("Invalid email or password");
            }

            string name = staff.FullName;
            string email = staff.User.Email;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync("LocalCookie", new ClaimsPrincipal(claimsIdentity), authProperties);

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
            await HttpContext.SignOutAsync("LocalCookie");
            
            return Redirect("/Login");
        }

    }
}
