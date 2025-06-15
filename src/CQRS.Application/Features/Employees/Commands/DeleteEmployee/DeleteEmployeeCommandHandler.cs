using CQRS.Application.Common.Interfaces;
using CQRS.Domain.Interfaces;

namespace CQRS.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _repository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(DeleteEmployeeCommand command)
    {
        if (command.EmployeeId <= 0)
            throw new Common.Exceptions.ValidationException("Employee ID must be greater than 0");

        return await _repository.DeleteAsync(command.EmployeeId);
    }
}