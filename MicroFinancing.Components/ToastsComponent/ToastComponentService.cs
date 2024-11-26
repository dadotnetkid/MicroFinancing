using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Components.ToastsComponent
{
    public class ToastComponentService : IToasts
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ToastContainer container { get; set; }
        public ToastComponentService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task ShowToast(string title, string message)
        {
            try
            {
                if (container is null)
                {
                    return;
                }
                await container.ShowToast(title, message);
            }
            catch (Exception e)
            {

            }
        }
    }
}
