using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto?> HandleAsync(GetEmployeeByIdQuery query)
    {
        if (query.EmployeeId <= 0)
            return null;

        var employee = await _repository.GetByIdAsync(query.EmployeeId);

        if (employee == null)
            return null;

        return new EmployeeDto
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
        };
    }
}