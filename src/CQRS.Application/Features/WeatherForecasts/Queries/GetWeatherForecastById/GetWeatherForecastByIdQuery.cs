using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Application.Features.WeatherForecasts.Queries.GetWeatherForecastById;

public class GetWeatherForecastByIdQuery : IQuery<WeatherForecastDto>
{
    public int Id { get; set; }

    public GetWeatherForecastByIdQuery(int id)
    {
        Id = id;
    }
}