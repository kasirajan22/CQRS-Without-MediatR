using Microsoft.EntityFrameworkCore;
using CQRS.Domain.Entities;
using CQRS.Domain.Enums;
using CQRS.Infrastructure.Data;

namespace CQRS.Infrastructure.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        try
        {
            // Try to apply any pending migrations first
            await context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            // If migrations fail (e.g., no migrations exist), ensure database is created
            await context.Database.EnsureCreatedAsync();
        }

        // Check if data already exists
        if (await context.WeatherForecasts.AnyAsync() || await context.Employees.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Seed WeatherForecast data
        var forecasts = new[]
        {
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 
                TemperatureC = 15, 
                Summary = "Cool",
                CreatedAt = DateTime.UtcNow
            },
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 
                TemperatureC = 22, 
                Summary = "Mild",
                CreatedAt = DateTime.UtcNow
            },
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)), 
                TemperatureC = 28, 
                Summary = "Warm",
                CreatedAt = DateTime.UtcNow
            },
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(4)), 
                TemperatureC = 35, 
                Summary = "Hot",
                CreatedAt = DateTime.UtcNow
            },
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(5)), 
                TemperatureC = 10, 
                Summary = "Chilly",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.WeatherForecasts.AddRange(forecasts);

        // Seed Employee data
        var employees = new[]
        {
            new Employee 
            { 
                FirstName = "John", 
                LastName = "Doe", 
                Gender = Gender.Male, 
                DOB = new DateOnly(1990, 5, 15), 
                EmailID = "john.doe@company.com", 
                PhoneNo = "+1-555-0101", 
                DOJ = new DateOnly(2020, 1, 15),
                CreatedAt = DateTime.UtcNow
            },
            new Employee 
            { 
                FirstName = "Jane", 
                LastName = "Smith", 
                Gender = Gender.Female, 
                DOB = new DateOnly(1985, 8, 22), 
                EmailID = "jane.smith@company.com", 
                PhoneNo = "+1-555-0102", 
                DOJ = new DateOnly(2019, 3, 10),
                CreatedAt = DateTime.UtcNow
            },
            new Employee 
            { 
                FirstName = "Michael", 
                LastName = "Johnson", 
                Gender = Gender.Male, 
                DOB = new DateOnly(1992, 12, 3), 
                EmailID = "michael.johnson@company.com", 
                PhoneNo = "+1-555-0103", 
                DOJ = new DateOnly(2021, 6, 1),
                CreatedAt = DateTime.UtcNow
            },
            new Employee 
            { 
                FirstName = "Sarah", 
                LastName = "Williams", 
                Gender = Gender.Female, 
                DOB = new DateOnly(1988, 4, 18), 
                EmailID = "sarah.williams@company.com", 
                PhoneNo = "+1-555-0104", 
                DOJ = new DateOnly(2018, 9, 5),
                CreatedAt = DateTime.UtcNow
            },
            new Employee 
            { 
                FirstName = "David", 
                LastName = "Brown", 
                Gender = Gender.Male, 
                DOB = new DateOnly(1995, 7, 11), 
                EmailID = "david.brown@company.com", 
                PhoneNo = "+1-555-0105", 
                DOJ = new DateOnly(2022, 2, 14),
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();
    }
}