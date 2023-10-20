using DineFine.API.Constants;

namespace DineFine.API.Services;

public class NotificationService : INotificationService
{
    private readonly EmailService _emailService;

    public NotificationService(EmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task OnOrderReceivedAsync(string orderId)
    {
        var subject = $"Order {orderId} Received";
        var body = $"The order with Id: {orderId} has been received.";
        const string fromEmail = NotificationConstants.WaiterEmail;
        const string toEmail = NotificationConstants.KitchenEmail;
        await _emailService.SendEmailAsync(fromEmail, toEmail, subject, body);
    }
    
    public async Task OnOrderReadyAsync(string orderId)
    {
        var subject = $"Order {orderId} Ready";
        var body = $"The order with Id: {orderId} has been received.";
        const string toEmail = NotificationConstants.WaiterEmail;
        const string fromEmail = NotificationConstants.KitchenEmail;
        await _emailService.SendEmailAsync(fromEmail, toEmail, subject, body);
    }
}