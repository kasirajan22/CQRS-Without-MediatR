namespace CQRS.Application.Features.WeatherForecasts.DTOs;

public class CreateWeatherForecastDto
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
}