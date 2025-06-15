using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CQRS.Domain.Entities;

namespace CQRS.Infrastructure.Data.Configurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.TemperatureC)
            .IsRequired();

        builder.Property(e => e.Summary)
            .HasMaxLength(100);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(100);

        // Ignore computed property
        builder.Ignore(e => e.TemperatureF);

        // Index for better query performance
        builder.HasIndex(e => e.Date);
    }
}