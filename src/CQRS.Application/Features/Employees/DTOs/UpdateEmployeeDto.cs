using CQRS.Domain.Enums;

namespace CQRS.Application.Features.Employees.DTOs;

public class UpdateEmployeeDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DOB { get; set; }
    public string EmailID { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public DateOnly DOJ { get; set; }
}