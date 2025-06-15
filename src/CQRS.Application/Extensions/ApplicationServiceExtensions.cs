using Microsoft.Extensions.DependencyInjection;
using CQRS.Application.Common.Dispatchers;
using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.Commands.CreateEmployee;
using CQRS.Application.Features.Employees.Commands.UpdateEmployee;
using CQRS.Application.Features.Employees.Commands.DeleteEmployee;
using CQRS.Application.Features.Employees.Queries.GetAllEmployees;
using CQRS.Application.Features.Employees.Queries.GetEmployeeById;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Application.Features.WeatherForecasts.Commands.CreateWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Commands.UpdateWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Commands.DeleteWeatherForecast;
using CQRS.Application.Features.WeatherForecasts.Queries.GetAllWeatherForecasts;
using CQRS.Application.Features.WeatherForecasts.Queries.GetWeatherForecastById;
using CQRS.Application.Features.WeatherForecasts.DTOs;

namespace CQRS.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add Dispatchers
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        // Add Validators
        services.AddScoped<CreateEmployeeCommandValidator>();
        services.AddScoped<UpdateEmployeeCommandValidator>();

        // Add Employee Handlers
        services.AddScoped<ICommandHandler<CreateEmployeeCommand, EmployeeDto>, CreateEmployeeCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateEmployeeCommand, EmployeeDto?>, UpdateEmployeeCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();
        services.AddScoped<IQueryHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>, GetAllEmployeesQueryHandler>();
        services.AddScoped<IQueryHandler<GetEmployeeByIdQuery, EmployeeDto?>, GetEmployeeByIdQueryHandler>();

        // Add WeatherForecast Handlers
        services.AddScoped<ICommandHandler<CreateWeatherForecastCommand, WeatherForecastDto>, CreateWeatherForecastCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateWeatherForecastCommand, WeatherForecastDto>, UpdateWeatherForecastCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteWeatherForecastCommand, bool>, DeleteWeatherForecastCommandHandler>();
        services.AddScoped<IQueryHandler<GetAllWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>, GetAllWeatherForecastsQueryHandler>();
        services.AddScoped<IQueryHandler<GetWeatherForecastByIdQuery, WeatherForecastDto>, GetWeatherForecastByIdQueryHandler>();

        return services;
    }
}