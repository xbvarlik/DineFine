namespace DineFine.API.Constants;

public abstract class NotificationConstants
{
    public const string WaiterEmail = "waiter.dinefine@gmail.com";
    public const string KitchenEmail = "kitchen.dinefine@gmail.com";
    public const string SystemEmail = "base.dinefine@gmail.com";
}

public abstract class OrderStatusCodes
{
    public const int Received = 1;
    public const int Preparing = 2;
    public const int Ready = 3;
    public const int Delivered = 4;
}