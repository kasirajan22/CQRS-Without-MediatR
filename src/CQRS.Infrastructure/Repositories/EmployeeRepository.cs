using Microsoft.EntityFrameworkCore;
using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Infrastructure.Data;

namespace CQRS.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int employeeId)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> UpdateAsync(int employeeId, Employee employee)
    {
        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeID == employeeId);

        if (existingEmployee == null)
            return null;

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Gender = employee.Gender;
        existingEmployee.DOB = employee.DOB;
        existingEmployee.EmailID = employee.EmailID;
        existingEmployee.PhoneNo = employee.PhoneNo;
        existingEmployee.DOJ = employee.DOJ;
        existingEmployee.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingEmployee;
    }

    public async Task<bool> DeleteAsync(int employeeId)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeID == employeeId);

        if (employee == null)
            return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}