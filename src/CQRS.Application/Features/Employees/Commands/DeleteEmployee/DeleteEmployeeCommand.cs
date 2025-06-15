using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : ICommand<bool>
{
    public int EmployeeId { get; set; }
}