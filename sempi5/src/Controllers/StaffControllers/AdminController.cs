using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sempi5.Domain.Staff.DTOs;
using Sempi5.Domain.Staff;

namespace Sempi5.Controllers.StaffControlers
{

    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to the Admin Page");
        }

    }
}