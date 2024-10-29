using Microsoft.AspNetCore.Mvc;

namespace Sempi5.Controllers;

[Route("error")]
[ApiController]
public class ErrorController : ControllerBase
{
    [Route("access-denied")]
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return new JsonResult(new { message = "Access denied. Dont have the necessary permissions." })
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }
}