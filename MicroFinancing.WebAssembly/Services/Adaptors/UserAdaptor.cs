using System.Text.Json;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.WebAssembly.Services.Adaptors
{
    public class UserAdaptor : DataAdaptor
    {
        private readonly IUserClient _userClient;

        public UserAdaptor(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string? key = null)
        {
            try
            {
                var res = await _userClient.GetUsersAsync(JsonSerializer.Serialize(dataManagerRequest));

                if (!dataManagerRequest.RequiresCounts)
                {
                    return res.Result;
                }

                return new DataResult()
                {
                    Result = res.Result,
                    Count = res.Count
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }
    }
}