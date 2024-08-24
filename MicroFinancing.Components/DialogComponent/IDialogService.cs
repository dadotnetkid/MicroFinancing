using MicroFinancing.Components.ToastsComponent;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Components.DialogComponent;

public interface IDialogService
{
    public void ShowDialog(string title,
                           string message,
                           Action<bool> callBack);
    public DialogContainer container { get; set; }
}