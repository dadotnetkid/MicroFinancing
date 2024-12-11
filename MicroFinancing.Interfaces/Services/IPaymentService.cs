using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.Interfaces.Services;

public interface IPaymentService
{
    IQueryable<PaymentGridDTM> Get();

    Task<Payment> AddPayment(CreatePaymentDTM model);
    
    Task UploadFile(UploadFiles uploadedFile, Payment payment);
    Task UploadFile(byte[]? uploadedFile, long paymentId);

    Task DeletePayment(long paymentId);

    Task ChangePayment(long paymentId,
                       long lendingId);

    Task<object> GetPaymentForApproval(DataManagerRequest dm);

    Task PaymentApproval(PaymentsForApprovalByDateDto item);

    Task<List<PaymentGridDTM>> GetPaymentByCollectorId(string? userId);
}