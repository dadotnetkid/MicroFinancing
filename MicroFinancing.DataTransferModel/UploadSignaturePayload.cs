using MicroFinancing.Entities;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.DataTransferModel;

public class UploadSignaturePayload
{
    public byte[] UploadFiles { get; set; }
    public long PaymentId { get; set; }
}