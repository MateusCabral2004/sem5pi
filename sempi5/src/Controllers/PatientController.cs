using Microsoft.AspNetCore.Mvc;

namespace Sempi5.Controllers;

[ApiController]
[Route("api/email")]
public class PatientController : ControllerBase
{
    // Endpoint to track email click
    [HttpGet("track-email-click")]
    public IActionResult TrackEmailClick(string email, string token)
    {
        // Register that the link was clicked
        Console.WriteLine($"O link foi acessado pelo email: {email} com o token: {token} em {DateTime.UtcNow}");
        
        // Redirect to the confirmation link
        return Ok($"Acesso registrado para o email: {email}. Obrigado por confirmar.");
    }
}