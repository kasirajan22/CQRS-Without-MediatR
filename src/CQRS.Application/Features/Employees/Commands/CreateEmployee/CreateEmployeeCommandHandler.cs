using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeRepository _repository;
    private readonly CreateEmployeeCommandValidator _validator;

    public CreateEmployeeCommandHandler(IEmployeeRepository repository, CreateEmployeeCommandValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<EmployeeDto> HandleAsync(CreateEmployeeCommand command)
    {
        // Validate
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new Common.Exceptions.ValidationException(validationResult.Errors);
        }

        // Map to entity
        var employee = new Employee
        {
            FirstName = command.FirstName.Trim(),
            LastName = command.LastName.Trim(),
            Gender = command.Gender,
            DOB = command.DOB,
            EmailID = command.EmailID.Trim().ToLower(),
            PhoneNo = command.PhoneNo.Trim(),
            DOJ = command.DOJ,
            CreatedAt = DateTime.UtcNow
        };

        // Save
        var createdEmployee = await _repository.CreateAsync(employee);

        // Map to DTO
        return MapToDto(createdEmployee);
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
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