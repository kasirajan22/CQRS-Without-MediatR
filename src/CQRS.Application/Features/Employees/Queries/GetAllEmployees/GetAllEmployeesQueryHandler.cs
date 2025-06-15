using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeRepository _repository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmployeeDto>> HandleAsync(GetAllEmployeesQuery query)
    {
        var employees = await _repository.GetAllAsync();

        return employees.Select(employee => new EmployeeDto
        {
            EmployeeID = employee.EmployeeID,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            FullName = employee.FullName,
            Gender = employee.Gender,
            GenderDisplay = employee.Gender.ToString(),
            DOB = employee.DOB,
            EmailID = employee.EmailID,
            PhoneNo = employee.PhoneNo,
            DOJ = employee.DOJ,
            Age = employee.Age,
            YearsOfService = employee.YearsOfService,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        });
    }
}