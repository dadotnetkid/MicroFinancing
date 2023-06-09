using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using SQLite;

namespace MicroFinancing.Mobile.Services;

internal sealed class CustomerService
{
    private readonly HttpClient _client;
    private readonly UserService _userService;
    private readonly SQLiteAsyncConnection _db;

    public CustomerService(HttpClient client,
        UserService userService,
        SQLiteAsyncConnection db)
    {
        _client = client;
        _userService = userService;
        _db = db;

    }

    public async Task<List<Customers>> GetCustomers(bool online,NavigationManager navigationManager)
    {
        if (online)
        {
            var user = await _userService.GetUserDetail();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.StringToken}");
            var res = await _client.GetAsync("/api/customers/GetCustomers");
            if (res.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                await _userService.Logout();
                navigationManager.NavigateTo("/");

                return new();
            }

            var result =await res.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<List<Customers>>(result);

            await _db.DeleteAllAsync<Customers>();
            await _db.InsertAllAsync(customer);
        }



        return await _db.Table<Customers>().ToListAsync();
    }
}