using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Services;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
public class SystemUserController : ControllerBase
{
    private readonly SystemUserService _adminService;
    private readonly EmailService _sendEmailService;
    
    public SystemUserController(SystemUserService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpPost("registerStaff")]
    public async Task<IActionResult> RegisterStaff(RegisterUserDTO user)
    {
        try
        {
            Console.WriteLine("StaffId: " + user.staffOrStaffId);
            var responseDto = await _adminService.RegisterUser(user);
            await _sendEmailService.SendStaffConfirmationEmail(responseDto.email, responseDto.staffOrStaffId);
            return Ok("Staff member registered successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}