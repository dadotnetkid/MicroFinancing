using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.Interfaces.Services;

public interface IPaymentService
{
    IQueryable<PaymentGridDTM> Get();

    Task<Payment> AddPayment(CreatePaymentDTM model);
    
    Task UploadFile(UploadFiles uploadedFile, Payment payment);
}