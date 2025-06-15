using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Commands.DeleteWeatherForecast;

public class DeleteWeatherForecastCommand : ICommand<bool>
{
    public int Id { get; set; }

    public DeleteWeatherForecastCommand(int id)
    {
        Id = id;
    }
}