using CQRS.Application.Common.Exceptions;
using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Commands.UpdateWeatherForecast;

public class UpdateWeatherForecastCommandHandler : ICommandHandler<UpdateWeatherForecastCommand, WeatherForecastDto>
{
    private readonly IWeatherForecastRepository _repository;

    public UpdateWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<WeatherForecastDto> HandleAsync(UpdateWeatherForecastCommand command)
    {
        var weatherForecast = new WeatherForecast
        {
            Date = command.Date,
            TemperatureC = command.TemperatureC,
            Summary = command.Summary
        };

        var updatedWeatherForecast = await _repository.UpdateAsync(command.Id, weatherForecast);
        
        if (updatedWeatherForecast == null)
        {
            throw new NotFoundException($"Weather forecast with ID {command.Id} not found.");
        }

        return new WeatherForecastDto
        {
            Id = updatedWeatherForecast.Id,
            Date = updatedWeatherForecast.Date,
            TemperatureC = updatedWeatherForecast.TemperatureC,
            TemperatureF = updatedWeatherForecast.TemperatureF,
            Summary = updatedWeatherForecast.Summary ?? string.Empty,
            CreatedAt = updatedWeatherForecast.CreatedAt,
            UpdatedAt = updatedWeatherForecast.UpdatedAt
        };
    }
}