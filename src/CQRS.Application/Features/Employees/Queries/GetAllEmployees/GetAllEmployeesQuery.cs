using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;

namespace CQRS.Application.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IQuery<IEnumerable<EmployeeDto>>
{
}