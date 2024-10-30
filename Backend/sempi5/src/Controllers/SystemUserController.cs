using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Services;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SystemUserController : ControllerBase
{
    private readonly SystemUserService _userService;
    private readonly EmailService _sendEmailService;
    
    public SystemUserController(SystemUserService userService, EmailService sendEmailService)
    {
        _userService = userService;
        _sendEmailService = sendEmailService;
    }
    
    [HttpPost("registerStaff")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterStaff(RegisterUserDTO user)
    {
        try
        {
            if(user.role == "Patient")
            {
                return BadRequest("Invalid role");
            }
            var responseDto = await _userService.RegisterUser(user);
            await _sendEmailService.SendStaffConfirmationEmail(responseDto.email, responseDto.staffOrStaffId);
            return Ok("Staff member registered successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}