using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;
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
        // Email credentials
        //TODO encript credentials
        {
            // Email credentials
            //TODO encript credentials
             // var username = "job4u_g4@outlook.com";
             // var password = "sem4pi_g4";
            var username = _configuration["SmtpSettings:Username"];
            var password = _configuration["SmtpSettings:Password"];

            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
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
                return $"Email sent to {email} with subject 'Email Confirmation'";
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
    public async Task SendPatientConfirmationEmail(string email, string token)
    {
        var body = $"Please confirm your email by clicking " +
                   $"<a href='http://localhost:5001/confirmToken/patient/{token}'>here</a>";
        var subject = "Email Confirmation";
        await SendEmailAsync(email, body, subject);
    }
}