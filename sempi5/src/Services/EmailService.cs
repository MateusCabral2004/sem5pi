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
    public EmailService(IConfiguration configuration, IConfirmationTokenRepository confirmationRepository, IUnitOfWork unitOfWork)
    {
        _confirmationRepository = confirmationRepository;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        
    }
    

    public async Task<string> SendEmailAsync(string email)
    {
        // Email credentials
        //TODO encript credentials
        var username = "1221121@isep.ipp.pt";
        var password = "";

        var confirmationToken = new ConfirmationToken(new Email(email));
        // await _confirmationRepository.saveAsyc(confirmationToken);
        // Console.WriteLine("Token: " + confirmationToken.Id);
        
        var smtpClient = new SmtpClient("smtp.office365.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password),
        };

        // Updated tracking link to a different endpoint
        var trackingLink = $"http://localhost:5001/patient/email/track-email-click?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(confirmationToken.Id.ToString())}";

        var body = $@"
            <p>Dear user,</p>
            <p>Please confirm your email by clicking the following link:</p>
            <a href='{trackingLink}'>Confirm Email</a>
            <p>If you did not request this, please ignore this email.</p>";

        var mailMessage = new MailMessage
        {
            From = new MailAddress(username),
            Subject = "Email Confirmation",
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