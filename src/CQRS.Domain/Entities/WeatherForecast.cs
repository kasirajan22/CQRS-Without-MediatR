using CQRS.Domain.Entities.Common;

namespace CQRS.Domain.Entities;

public class WeatherForecast : BaseEntity
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }

    // Computed property
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}