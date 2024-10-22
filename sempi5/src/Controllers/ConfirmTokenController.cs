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
 
    [HttpGet("{token}")]
    public async Task<IActionResult> ConfirmToken(string token)
    {
        await tokenService.ConfirmAccount(token);
        return Ok("Token Confirmed: " + token);
    }
}