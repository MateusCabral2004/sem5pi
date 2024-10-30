using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sempi5.Services;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public class ConfirmTokenController : ControllerBase
{
    private readonly TokenService tokenService;

    public ConfirmTokenController(TokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [HttpGet("staff/{token}")]
    public async Task<IActionResult> ConfirmStaffToken(string token)
    {
        try
        {
            await tokenService.ConfirmStaffAccount(token);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }

        return Ok("Token Confirmed: " + token);
    }
    [HttpGet("patient/{token}")]
    public async Task<IActionResult> ConfirmPatientToken(string token)
    {
        try
        {
            await tokenService.confirmPatientAccount(token);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }

        return Ok("Token Confirmed: " + token);
    }
}