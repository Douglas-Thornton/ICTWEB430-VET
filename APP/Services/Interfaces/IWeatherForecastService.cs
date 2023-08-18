using APP.Data.Models;

namespace APP.Data.Services;
public interface IWeatherForecastService
{
    /// <summary>
    /// Returns the Forcast for the weather
    /// </summary>
    /// <param name="startDate"></param>
    /// <returns></returns>
    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);

}
