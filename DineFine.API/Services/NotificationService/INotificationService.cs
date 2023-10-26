namespace DineFine.API.Services;

public interface INotificationService
{
    Task OnOrderReadyAsync(string orderId);
    Task OnOrderReceivedAsync(string orderId);
    Task OnStockLowAsync(string ingredientName);
}