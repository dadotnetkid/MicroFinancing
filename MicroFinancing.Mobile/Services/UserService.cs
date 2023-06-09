using SQLite;

namespace MicroFinancing.Mobile.Services;

internal sealed class UserService
{
    private readonly SQLiteAsyncConnection _db;

    public UserService(SQLiteAsyncConnection db)
    {
        _db = db;
    }

    public async Task<Users> GetUserDetail()
    {
        return await _db.Table<Users>().FirstOrDefaultAsync();
    }

    public async Task<bool> IsExpired()
    {
        var user = await GetUserDetail();
        if (user is null) return true;
        if (user.DateTo < DateTime.UtcNow)
        {
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        await _db.DeleteAllAsync<Users>();
    }
}