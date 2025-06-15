using CQRS.Domain.Entities;

namespace CQRS.Domain.Interfaces;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<WeatherForecast?> GetByIdAsync(int id);
    Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast);
    Task<WeatherForecast?> UpdateAsync(int id, WeatherForecast weatherForecast);
    Task<bool> DeleteAsync(int id);
}