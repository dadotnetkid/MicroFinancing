namespace MicroFinancing.Services;

public interface ISmsService
{
    Task SendSms(string phoneNumber, string messages);
    Task SendNewlyCreateCustomer(string phoneNumber, string customerName);
    Task SendPaymentConfirmation(long customerId,
                                 Payment payment);
    Task SendRestructureToAdmin(string? customers);
    Task SendRestructureToCustomer(string phoneNumber, string customerName);

    Task PaymentConfirmation(long customerId,
                             Payment payment);
}