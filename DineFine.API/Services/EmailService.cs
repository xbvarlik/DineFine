using System.Net;
using System.Net.Mail;
using DineFine.API.Constants;

namespace DineFine.API.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string fromEmail, string toEmailAddress, string subject, string body)
    {
        var host = _configuration["EmailSettings:Host"];
        var fromPassword = _configuration["EmailSettings:FromPassword"];

        var smtpClient = new SmtpClient();
        
        smtpClient.Host = host!;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
        smtpClient.EnableSsl = true;
        
        var message = new MailMessage();
        
        message.From = new MailAddress(fromEmail!);
        message.To.Add(toEmailAddress);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;
        
        await smtpClient.SendMailAsync(message);
    }
    
    public async Task SendResetPasswordEmail(string resetPasswordLink, string toEmailAddress)
    {
        var fromEmail = _configuration["EmailSettings:FromEmail"]!;
        const string subject = "Reset Password";
        var body = $"To reset your password, <a href='{resetPasswordLink}'>click here</a>";
        await SendEmailAsync(fromEmail, toEmailAddress, subject, body);
    }
}