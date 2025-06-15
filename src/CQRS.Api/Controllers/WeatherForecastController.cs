using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.WeatherForecasts.Commands.CreateWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Commands.UpdateWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Commands.DeleteWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Queries.GetAllWeatherForecasts;
using CQRS.Application.Features.WeatherForecasts.Queries.GetWeatherForecastById;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public WeatherForecastController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetAll()
    {
        var query = new GetAllWeatherForecastsQuery();
        var result = await _queryDispatcher.DispatchAsync(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherForecastDto>> GetById(int id)
    {
        var query = new GetWeatherForecastByIdQuery(id);
        var result = await _queryDispatcher.DispatchAsync(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<WeatherForecastDto>> Create([FromBody] CreateWeatherForecastDto dto)
    {
        var command = new CreateWeatherForecastCommand
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary
        };

        var result = await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WeatherForecastDto>> Update(int id, [FromBody] UpdateWeatherForecastDto dto)
    {
        var command = new UpdateWeatherForecastCommand
        {
            Id = id,
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary
        };

        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var command = new DeleteWeatherForecastCommand(id);
        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result);
    }
}