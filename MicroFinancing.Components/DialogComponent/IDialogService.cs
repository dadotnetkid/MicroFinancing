using MicroFinancing.Components.ToastsComponent;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Components.DialogComponent;

public interface IDialogService
{
    public Task<bool> ShowDialog(string title,
                           string message);
    public DialogContainer container { get; set; }
}