using Microsoft.AspNetCore.Mvc;
using Sempi5.Services;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
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
}