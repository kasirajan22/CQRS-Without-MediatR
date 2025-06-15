using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Commands.CreateWeatherForecast;

public class CreateWeatherForecastCommandHandler : ICommandHandler<CreateWeatherForecastCommand, WeatherForecastDto>
{
    private readonly IWeatherForecastRepository _repository;

    public CreateWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<WeatherForecastDto> HandleAsync(CreateWeatherForecastCommand command)
    {
        var weatherForecast = new WeatherForecast
        {
            Date = command.Date,
            TemperatureC = command.TemperatureC,
            Summary = command.Summary
        };

        var createdWeatherForecast = await _repository.CreateAsync(weatherForecast);

        return new WeatherForecastDto
        {
            Id = createdWeatherForecast.Id,
            Date = createdWeatherForecast.Date,
            TemperatureC = createdWeatherForecast.TemperatureC,
            TemperatureF = createdWeatherForecast.TemperatureF,
            Summary = createdWeatherForecast.Summary ?? string.Empty,
            CreatedAt = createdWeatherForecast.CreatedAt,
            UpdatedAt = createdWeatherForecast.UpdatedAt
        };
    }
}