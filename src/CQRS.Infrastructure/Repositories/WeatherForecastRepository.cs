using Microsoft.EntityFrameworkCore;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Infrastructure.Data;

namespace CQRS.Infrastructure.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ApplicationDbContext _context;

    public WeatherForecastRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await _context.WeatherForecasts
            .OrderBy(w => w.Date)
            .ToListAsync();
    }

    public async Task<WeatherForecast?> GetByIdAsync(int id)
    {
        return await _context.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Add(weatherForecast);
        await _context.SaveChangesAsync();
        return weatherForecast;
    }

    public async Task<WeatherForecast?> UpdateAsync(int id, WeatherForecast weatherForecast)
    {
        var existingForecast = await _context.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id);

        if (existingForecast == null)
            return null;

        existingForecast.Date = weatherForecast.Date;
        existingForecast.TemperatureC = weatherForecast.TemperatureC;
        existingForecast.Summary = weatherForecast.Summary;
        existingForecast.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingForecast;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var weatherForecast = await _context.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id);

        if (weatherForecast == null)
            return false;

        _context.WeatherForecasts.Remove(weatherForecast);
        await _context.SaveChangesAsync();
        return true;
    }
}