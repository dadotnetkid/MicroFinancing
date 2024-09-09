using System.Text;
using System.Text.Unicode;
using MicroFinancing.MobileApp.Services.Client;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.MobileApp.Components.Pages;

public partial class AddPayment
{
    [Parameter] public long CustomerId { get; set; }
    [Inject] private IPaymentClient PaymentClient { get; set; }
    [Inject] private ICustomersClient CustomersClient { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    private CreatePaymentDTM model = new();
    private SfSignature signatureRef;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            CustomerDetail = await CustomersClient.GetCustomerIdAsync(CustomerId);
            model.PaymentDate = DateTime.Now;
            StateHasChanged();
        }
    }

    protected override void OnInitialized()
    {
        model.PaymentDate = DateTime.Now;
        base.OnInitialized();
    }

    public CustomerGridDTM CustomerDetail { get; set; } = new();

    private async Task OnChange(SignatureChangeEventArgs obj)
    {
        await signatureRef.SaveAsync();
    }

    private void BackToList()
    {
        NavigationManager.NavigateTo("/", true);
    }

    private async Task SavePayment()
    {
        try
        {
            model.CustomerId = CustomerId;
            model.Reason = "Payment";
            model.CreatedAt = DateTimeOffset.Now;

            var payment = await PaymentClient.AddPaymentAsync(model);
            var signature = await signatureRef.GetSignatureAsync(SignatureFileType.Png);
            await PaymentClient.UploadSignatureAsync(new UploadSignaturePayload()
            {
                PaymentId = payment,
                UploadFiles = Convert.FromBase64String(signature.Replace("data:image/png;base64,", string.Empty))
            });

            BackToList();
        }
        catch (Exception e)
        {
        }
    }
}