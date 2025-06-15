using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.DTOs;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, EmployeeDto?>
{
    private readonly IEmployeeRepository _repository;
    private readonly UpdateEmployeeCommandValidator _validator;

    public UpdateEmployeeCommandHandler(IEmployeeRepository repository, UpdateEmployeeCommandValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<EmployeeDto?> HandleAsync(UpdateEmployeeCommand command)
    {
        // Validate
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new Common.Exceptions.ValidationException(validationResult.Errors);
        }

        // Create updated employee entity
        var employee = new Employee
        {
            FirstName = command.FirstName.Trim(),
            LastName = command.LastName.Trim(),
            Gender = command.Gender,
            DOB = command.DOB,
            EmailID = command.EmailID.Trim().ToLower(),
            PhoneNo = command.PhoneNo.Trim(),
            DOJ = command.DOJ,
            UpdatedAt = DateTime.UtcNow
        };

        // Update
        var updatedEmployee = await _repository.UpdateAsync(command.EmployeeId, employee);

        if (updatedEmployee == null)
            return null;

        // Map to DTO
        return MapToDto(updatedEmployee);
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