using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Application.Features.WeatherForecasts.Commands.CreateWeatherForecast;

public class CreateWeatherForecastCommand : ICommand<WeatherForecastDto>
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
}