using MauiApp1.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal class WeatherForecastService
    {
        private readonly SQLiteAsyncConnection _db;

        public WeatherForecastService(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public async Task<IList<WeatherForecast>> Get()
        {
            return await _db.Table<WeatherForecast>().ToListAsync();
        }

        public async Task Insert(WeatherForecast forecastModel)
        {
            var weather = await _db.InsertAsync(forecastModel);
        }
    }
}
