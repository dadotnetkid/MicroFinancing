using MicroFinancing.Components.ToastsComponent;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Components.DialogComponent
{
    public class DialogComponentService : IDialogService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DialogContainer container { get; set; }
        public DialogComponentService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public void ShowDialog(string title,
                               string message,
                               Action<bool> callBack)
        {
            container.ShowToast(title, message,callBack);
        }
    }
}
