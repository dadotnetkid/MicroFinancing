namespace MicroFinancing.Components.ToastsComponent;

public interface IToasts
{
    public Task ShowToast(string title, string message);
    public ToastContainer container { get; set; }
}