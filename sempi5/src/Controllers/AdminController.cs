using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff.DTOs;
using Sempi5.Domain.Staff;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Services;
    
namespace Sempi5.Controllers
{   
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
    
        public AdminController(AdminService staffService)
        {
            _adminService = staffService;
        }
    
        [HttpGet]
        public IActionResult Home()
        {
            return Ok("Welcome to the Admin Page");
        }
    
        [HttpPost("registerStaff")]
        public async Task<IActionResult> RegisterStaff(SystemUserDTO user)
        {
            try
            { 
                await _adminService.RegisterUser(user);
                return Ok("Staff member registered successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
    }
}