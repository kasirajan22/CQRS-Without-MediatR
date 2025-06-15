using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Domain.Enums;

namespace CQRS.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : ICommand<EmployeeDto?>
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DOB { get; set; }
    public string EmailID { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public DateOnly DOJ { get; set; }
}