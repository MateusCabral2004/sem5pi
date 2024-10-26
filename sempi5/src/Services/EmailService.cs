using System.Text.Json;
using Sempi5.Domain.Encrypt;
using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Infrastructure.ConfirmationTokenRepository;

namespace Sempi5.Services;

using System.Net;
using System.Net.Mail;

public class EmailService
{
    // Private field to hold the configuration settings
    private readonly IConfiguration _configuration;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUnitOfWork _unitOfWork;


    // Constructor to initialize the EmailService with configuration settings
    public EmailService(IConfiguration configuration, IConfirmationTokenRepository confirmationRepository,
        IUnitOfWork unitOfWork)
    {
        _confirmationRepository = confirmationRepository;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }


    public async Task<string> SendEmailAsync(string email, string body, string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("Email subject cannot be null or empty.");
        }
        
        {
            var username = _configuration["SmtpSettingsGmail:Username"];
            var password = _configuration["SmtpSettingsGmail:Password"];
            var host = _configuration["SmtpSettingsGmail:Host"];
            var port = int.Parse(_configuration["SmtpSettingsGmail:Port"]);
            var enableSsl = bool.Parse(_configuration["SmtpSettingsGmail:EnableSsl"]);

            var smtpClient = new SmtpClient(host)
            {
                Port = port,
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(username, password),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(username),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return $"Email sent to {email}.";
            }
            catch (SmtpException e)
            {
                return $"Failed to send email to {email}\nReason: {e.Message}";
            }
        }
    }
    
    public async Task SendStaffConfirmationEmail(string email, string token)
    {
        var body = $"Please confirm your email by clicking " +
                   $"<a href='http://localhost:5001/confirmToken/staff/{token}'>here</a>";
        var subject = "Email Confirmation";
        await SendEmailAsync(email, body, subject);
    }
    
    private async Task SendEditStaffProfileConfirmationEmail(string email, string token)
    {
        var body = $"Please confirm your email by clicking " +
                   $"<a href='http://localhost:5001/confirmToken/staff/{token}'>here</a>";
        var subject = "Please Confirm Your Profile Changes";
        await SendEmailAsync(email, body, subject);
    }
    
    
    public async Task SendPatientConfirmationEmail(string email, string token)
    {
        var body = $"Please confirm your email by clicking " +
                   $"<a href='http://localhost:5001/confirmToken/patient/{token}'>here</a>";
        var subject = "Email Confirmation";
        await SendEmailAsync(email, body, subject);
    }
    public async Task SendPatientDeleteConfirmationEmail(string email, string token)
    {
        var body = $"Please confirm your email by clicking " +
                   $"<a href='http://localhost:5001/patient/account/exclude/confirm/{token}'>here</a>";
        var subject = "Delete accout Confirmation";
        await SendEmailAsync(email, body, subject);
    }

    public async Task SendPatientUpdatingEmail_EmailAltered(string oldEmail, string currentEmail)
    {
        var body = $"The email address associated to your account has been altered. Your current email" +
                   $"is "+ currentEmail;
        var subject = "Email Address Alteration";
        await SendEmailAsync(oldEmail, body, subject);
    }

    public async Task SendPatientUpdatingEmail_PhoneNumberAltered(string email, string phoneNumber)
    {
        var body = $"The phone number associated to your account has been altered. You current phone number is" +
                   $"registered as" + phoneNumber;
        var subject = $"Phone Number Alteration";
        await SendEmailAsync(email, body, subject);
    }
    
    public async Task PrepareEditStaffConfirmationEmail(string email, EditStaffDTO editStaffDto)
    {

        string serializedDto = JsonSerializer.Serialize(editStaffDto);
            
            var cryptography = new Cryptography();
             var encryptedString = cryptography.EncryptString(serializedDto);
            
        var link = $"http://localhost:5002/staff/editStaffProfile/{encryptedString}";
            
        var body = $@"
            <p>Please confirm this email by clicking the following link:</p>
            <a href='{link}'>Confirm Email</a>
            <p>If you did not request this, please ignore this email.</p>";

            
        await SendEmailAsync(email, body, "Please Confirm Your Profile Changes");
    }
    
}