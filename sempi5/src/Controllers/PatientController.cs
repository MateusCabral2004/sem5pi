using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public string getEmail()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        return claimsIdentity?.FindFirst(ClaimTypes.Email).Value;
    }

    [HttpGet("register")]
    [Authorize(Roles = "Unregistered")] //patient only  - falar com mateus para criar novo role
    public IActionResult Register()
    {
        return Ok("Por favor, forneça o seu número.");
    }

    [HttpPost("register")]
    [Authorize(Roles = "Unregistered")] //patient only  - falar com mateus para criar novo role
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

            var subject = "Email Confirmation";


            return Ok($"Número de registro {number} registrado com sucesso para o email: {email}.");
        }
        else
        {
            return BadRequest("Erro ao registrar número.");
        }
    }

    [HttpGet("account/appointment")]
    [Authorize(Roles = "Patient")] //patient only  - falar com mateus para criar novo role
    public async Task<IActionResult> listAppointments(string email)

    {
        
        
        var appointments = await patientService.appointmentList(email);
        if (appointments == null)
        {
            return BadRequest("Unauthorized acess(you need to confirm your account)");
        }

        return Ok(appointments);
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
    [Authorize(Roles = "Patient")] //patient only  - falar com mateus para criar novo role
    public async Task<IActionResult> excludeAccount(string email)
    {
       // await patientService.defineDataToExcludeAccount(email);
        return Ok("Account excluded");
    }

    [HttpPost("account/update")]
    [Authorize(Roles = "Patient")] //patient only  - falar com mateus para criar novo role
    public async Task<IActionResult> updateAccount(PatientProfileDto profileDto)
    {
        Console.WriteLine("Iniciando Update");
        //criar um token para eter no link
        if (profileDto.email != null || profileDto.phoneNumber != null)
        {
            string serializedDto = JsonSerializer.Serialize(profileDto);
            Console.WriteLine("Serialized DTO: " + serializedDto);
            await SendUpdateConfirmationEmail(getEmail(), $"http://localhost:5001/patient/account/update/{serializedDto}", "Update Confirmation");
            return Ok("Email sent to confirm update");
        }

        //adicionar um novo parametro que é o email do usuario logado
        await patientService.updateAccount(profileDto,getEmail());
        return Ok("Account updated");
    }

    [HttpGet("account/update/{jsonString}")]
    public async Task<IActionResult> updateAccounlt(string jsonString)
    {
        // Serializing the DTO
        PatientProfileDto profileDto;
        try
        {
            profileDto = JsonSerializer.Deserialize<PatientProfileDto>(jsonString);
        }
        catch (JsonException ex)
        {
            // Handle deserialization error
            return BadRequest($"Invalid JSON format: {ex.Message}");
        }

        Console.WriteLine("Iniciado update depois da confirmação de email");

        await patientService.updateAccount(profileDto,getEmail());

        return Ok("Account updated");
    }

    private async Task SendUpdateConfirmationEmail(string email, string link, string subject)
    {
        var body = $@"
        <p>Dear user,</p>
        <p>Please confirm this email by clicking the following link:</p>
        <a href='{link}'>Confirm Email</a>
        <p>If you did not request this, please ignore this email.</p>";
        
        await emailService.SendEmailAsync(email, body, subject);
    }
}