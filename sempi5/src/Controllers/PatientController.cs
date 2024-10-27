using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Services;
using ILogger = Serilog.ILogger;

namespace Sempi5.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientService patientService;
    private readonly EmailService emailService;
    private readonly CheckUserToDeleteService _checkUserToDeleteService;
    private readonly Serilog.ILogger _logger;
    
    public PatientController(PatientService patientService, EmailService emailService, CheckUserToDeleteService checkUserToDeleteService, ILogger logger)
    {
        this.patientService = patientService;
        this.emailService = emailService;
        _checkUserToDeleteService = checkUserToDeleteService;
        _logger = logger;
    }

    [HttpGet("checkUserToDelete")]
    public async Task<IActionResult> checkUserToDelete()
    {
        try
        {
            await _checkUserToDeleteService.checkUserToDelete();
            return Ok("Users deleted");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
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
    public async Task<IActionResult> RegisterNumber(int number)
    {
        if (number <= 0)
        {
            return BadRequest("Número de registro não pode ser vazio ou negativo.");
        }

        Console.WriteLine("Iniciando registro de conta com email: " + getEmail());
        var success = await patientService.RegisterPatientUser(getEmail(), number);

        if (success)
        {
            return Ok($"Número de registro {number} registrado com sucesso para o email: {getEmail()}.");
        }
        else
        {
            return BadRequest("Erro ao registrar número.");
        }
    }

    [HttpGet("account/appointment")]
    [Authorize(Roles = "Patient")] //patient only  - falar com mateus para criar novo role
    public async Task<IActionResult> listAppointments()

    {
        var appointments = await patientService.appointmentList(getEmail());
        if (appointments == null)
        {
            return BadRequest("Unauthorized acess(you need to confirm your account)");
        }

        Console.WriteLine("Listando agendamentos");
        Console.WriteLine(appointments);
        return Ok(appointments);
    }


    [HttpGet("account/exclude")]
     [Authorize(Roles = "Patient")]
    public async Task<IActionResult> excludeAccount()
    {
        await patientService.defineDataToExcludeAccount(getEmail());
        Console.WriteLine("Email to delete accout was sent");

        return Ok("We have sent email to confirm the exclusion");
    }

    [HttpGet("account/exclude/confirm/{token}")]
     [Authorize(Roles = "Patient")]
    public async Task<IActionResult> excludeAccountEmailConfirm(string token)
    {
        Console.WriteLine("Iniciando agendamento para exclusão de conta");

        try
        {
            await patientService.excludeAccountSchedule(token);
            return Ok("Account excluded");
        }
        catch (ArgumentException ex)
        {
            // token inválido
            Console.WriteLine("Erro de argumento: " + ex.Message);
            return BadRequest("Token inválido: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            // token não encontrado ou usuário não encontrado
            Console.WriteLine("Erro de operação inválida: " + ex.Message);
            return NotFound("Erro ao excluir a conta: " + ex.Message);
        }
        catch (Exception ex)
        {
            // erro inesperado
            Console.WriteLine("Erro inesperado: " + ex.Message);
            return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
        }
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
            await SendUpdateConfirmationEmail(getEmail(),
                $"http://localhost:5001/patient/account/update/{serializedDto}", "Update Confirmation");
            return Ok("Email sent to confirm update");
        }

        //adicionar um novo parametro que é o email do usuario logado
        await patientService.updateAccount(profileDto, getEmail());
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

        await patientService.updateAccount(profileDto, getEmail());

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

    [HttpGet("listPatientProfilesByName")]
    public async Task<IActionResult> ListPatientProfilesByName(NameDTO nameDto)
    {
        try
        {
            var patientProfile = await patientService.ListPatientByName(nameDto);
            return Ok(patientProfile);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listPatientProfilesByEmail")]
    public async Task<IActionResult> ListPatientProfilesByEmail(EmailDTO emailDto)
    {
        try
        {
            var patientProfiles = await patientService.ListPatientByEmail(emailDto);
            return Ok(patientProfiles);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listPatientProfilesByMedicalRecordNumber")]
    public async Task<IActionResult> ListPatientProfilesByMedicalRecordNumber(
        PatientIdDto patientId)
    {
        try
        {
            var patientProfiles = await patientService.ListPatientByMedicalRecordNumber(patientId);
            return Ok(patientProfiles);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("listPatientProfilesByDateOfBirth")]
    public async Task<IActionResult> ListPatientProfilesByDateOfBirth(DateDTO dateDto)
    {
        try
        {
            var patientProfiles = await patientService.ListPatientByDateOfBirth(dateDto);
            return Ok(patientProfiles);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("deletePatientProfile/{email}")]
    public async Task<IActionResult> DeletePatientProfile(string email)
    {
        try
        {
            await patientService.DeletePatientProfile(email);
            return Ok("Patient deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("editPatientProfile")]
    public async Task<IActionResult> EditPatientProfile(PatientDTO patientDto)
    {
        try
        {
            await patientService.EditPatientProfile(patientDto);
           // _logger.
            return Ok("Patient profile edited successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("registerPatientProfile")]
    public async Task<IActionResult> RegisterPatientProfile(PatientDTO patient)
    {
        try
        {
            await patientService.CreatePatientProfile(patient);
            return Ok("Patient profile created successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}