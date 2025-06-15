using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;

namespace CQRS.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IQuery<EmployeeDto?>
{
    public int EmployeeId { get; set; }
}