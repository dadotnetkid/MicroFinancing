namespace MicroFinancing.Services;

public interface ISmsService
{
    Task SendSms(string phoneNumber, string messages);
    Task SendNewlyCreateCustomer(string phoneNumber, string customerName);
    Task SendPaymentConfirmation(long customerId, string? amount);
}