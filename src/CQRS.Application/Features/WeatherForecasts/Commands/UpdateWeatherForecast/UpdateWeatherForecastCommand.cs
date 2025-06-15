using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Application.Features.WeatherForecasts.Commands.UpdateWeatherForecast;

public class UpdateWeatherForecastCommand : ICommand<WeatherForecastDto>
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
}