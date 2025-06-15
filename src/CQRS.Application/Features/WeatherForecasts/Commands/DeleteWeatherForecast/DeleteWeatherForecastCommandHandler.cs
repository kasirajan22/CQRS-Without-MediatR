using CQRS.Application.Common.Interfaces;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Commands.DeleteWeatherForecast;

public class DeleteWeatherForecastCommandHandler : ICommandHandler<DeleteWeatherForecastCommand, bool>
{
    private readonly IWeatherForecastRepository _repository;

    public DeleteWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(DeleteWeatherForecastCommand command)
    {
        return await _repository.DeleteAsync(command.Id);
    }
}