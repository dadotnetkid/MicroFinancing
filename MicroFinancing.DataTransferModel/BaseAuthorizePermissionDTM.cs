namespace MicroFinancing.DataTransferModel;

public class BaseAuthorizePermissionDTM
{
    public bool CanOverridePayment { get; set; }
    public bool CanAddPayment { get; set; }
    public bool CanAddLoan { get; set; }
    public bool CanSetFlag { get; set; }
    public bool CanPrint { get; set; }
}