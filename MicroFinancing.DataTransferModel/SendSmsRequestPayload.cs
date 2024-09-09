namespace MicroFinancing.DataTransferModel;

public class SendSmsRequestPayload
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
}