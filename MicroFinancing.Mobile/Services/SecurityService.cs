using MicroFinancing.Mobile.DTM;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroFinancing.Mobile.Services
{
    internal sealed class SecurityService
    {
        private readonly HttpClient _client;
        private readonly SQLiteAsyncConnection _db;

        public SecurityService(HttpClient client, SQLiteAsyncConnection db)
        {
            _client = client;
            _db = db;

        }

        public async Task<bool> Login(string userName, string password)
        {
            try
            {
                var res = await _client.PostAsJsonAsync("/api/security/login", new
                {
                    userName,
                    password
                });
                if (!res.IsSuccessStatusCode)
                {
                    return false;
                }

                var str = await res.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<Users>(str);
                await _db.DeleteAllAsync<Users>();
                await _db.InsertAsync(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
