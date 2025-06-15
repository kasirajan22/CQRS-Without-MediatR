using CQRS.Domain.Entities;

namespace CQRS.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int employeeId);
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee?> UpdateAsync(int employeeId, Employee employee);
    Task<bool> DeleteAsync(int employeeId);
}