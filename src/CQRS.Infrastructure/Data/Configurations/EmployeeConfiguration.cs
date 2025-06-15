using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CQRS.Domain.Entities;
using CQRS.Domain.Enums;

namespace CQRS.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.EmployeeID);
        
        builder.Property(e => e.EmployeeID)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Gender)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v));

        builder.Property(e => e.DOB)
            .IsRequired();

        builder.Property(e => e.EmailID)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNo)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.DOJ)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(100);

        // Ignore computed properties
        builder.Ignore(e => e.FullName);
        builder.Ignore(e => e.Age);
        builder.Ignore(e => e.YearsOfService);

        // Index for better query performance
        builder.HasIndex(e => e.EmailID)
            .IsUnique();

        builder.HasIndex(e => new { e.LastName, e.FirstName });
    }
}