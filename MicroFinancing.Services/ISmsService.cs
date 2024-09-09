namespace MicroFinancing.Services;

public interface ISmsService
{
    void SendSms(string phoneNumber, string messages);
    void SendNewlyCreateCustomer(string phoneNumber, string customerName);
    void SendPaymentConfirmation(long customerId, string? amount);
}