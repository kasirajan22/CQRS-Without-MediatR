using CQRS.Domain.Enums;

namespace CQRS.Application.Features.Employees.DTOs;

public class EmployeeDto
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string GenderDisplay { get; set; } = string.Empty;
    public DateOnly DOB { get; set; }
    public string EmailID { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public DateOnly DOJ { get; set; }
    public int Age { get; set; }
    public int YearsOfService { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}