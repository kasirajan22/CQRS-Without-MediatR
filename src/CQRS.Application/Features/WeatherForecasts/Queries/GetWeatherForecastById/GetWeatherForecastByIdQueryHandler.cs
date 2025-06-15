using CQRS.Application.Common.Exceptions;
using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Queries.GetWeatherForecastById;

public class GetWeatherForecastByIdQueryHandler : IQueryHandler<GetWeatherForecastByIdQuery, WeatherForecastDto>
{
    private readonly IWeatherForecastRepository _repository;

    public GetWeatherForecastByIdQueryHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<WeatherForecastDto> HandleAsync(GetWeatherForecastByIdQuery query)
    {
        var weatherForecast = await _repository.GetByIdAsync(query.Id);
        
        if (weatherForecast == null)
        {
            throw new NotFoundException($"Weather forecast with ID {query.Id} not found.");
        }

        return new WeatherForecastDto
        {
            Id = weatherForecast.Id,
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            TemperatureF = weatherForecast.TemperatureF,
            Summary = weatherForecast.Summary ?? string.Empty,
            CreatedAt = weatherForecast.CreatedAt,
            UpdatedAt = weatherForecast.UpdatedAt
        };
    }
}