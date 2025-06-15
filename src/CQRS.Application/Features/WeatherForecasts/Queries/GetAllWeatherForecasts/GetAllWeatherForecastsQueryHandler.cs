using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.WeatherForecasts.Queries.GetAllWeatherForecasts;

public class GetAllWeatherForecastsQueryHandler : IQueryHandler<GetAllWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>
{
    private readonly IWeatherForecastRepository _repository;

    public GetAllWeatherForecastsQueryHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeatherForecastDto>> HandleAsync(GetAllWeatherForecastsQuery query)
    {
        var weatherForecasts = await _repository.GetAllAsync();
        
        return weatherForecasts.Select(wf => new WeatherForecastDto
        {
            Id = wf.Id,
            Date = wf.Date,
            TemperatureC = wf.TemperatureC,
            TemperatureF = wf.TemperatureF,
            Summary = wf.Summary ?? string.Empty,
            CreatedAt = wf.CreatedAt,
            UpdatedAt = wf.UpdatedAt
        });
    }
}