using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Services;

namespace Sempi5.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientService patientService;
    private readonly EmailService emailService;


    public PatientController(PatientService patientService, EmailService emailService)
    {
        this.patientService = patientService;
        this.emailService = emailService;
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return Ok("Por favor, forneça o seu número.");
    }

    [HttpPost("register")]
    //TODO - Use email from the cookies (claim principal)
    public async Task<IActionResult> RegisterNumber(string email, int number)
    {
        if (number <= 0)
        {
            return BadRequest("Número de registro não pode ser vazio ou negativo.");
        }

        var success = await patientService.RegisterPatientUser(email, number);

        if (success)
        {
            emailService.SendEmailAsync(email);   
            return Ok($"Número de registro {number} registrado com sucesso para o email: {email}.");
            
        }
        else
        {
            return BadRequest("Erro ao registrar número.");
        }
    }
    [HttpGet("account/appointment")]
    public async Task listAppointments(string email)
    {
        var appointments = await patientService.appointmentList(email);
        if (appointments == null)
        {
            BadRequest("Unauthorized acess(you need to confirm your account)");
            return;
        }
        Ok(appointments);
    }

    [HttpGet("email/track-email-click")]
    public async Task<IActionResult> TrackEmailClick(string email, string token)
    {
        // Register that the link was clicked

        Console.WriteLine($"O link foi acessado pelo email: {email} com o token: {token} em {DateTime.UtcNow}");
        // Confirm the email
        await patientService.confirmEmail(email, token);


        return Ok($"Acesso registrado para o email: {email}. Obrigado por confirmar.");
    }

  

    [HttpPost("account/exclude")]
    public async Task<IActionResult> excludeAccount(string email)
    {
        await patientService.excludeAccount(email);
        return Ok("Account excluded");
    }

    [HttpPost("account/update")]
    public async Task<IActionResult> updateAccount(PatientProfileDto profileDto)
    {
       await patientService.updateAccount(profileDto);
        return Ok("Account updated");
    }

}