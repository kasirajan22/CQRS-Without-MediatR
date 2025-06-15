using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Application.Features.WeatherForecasts.Queries.GetAllWeatherForecasts;

public class GetAllWeatherForecastsQuery : IQuery<IEnumerable<WeatherForecastDto>>
{
}