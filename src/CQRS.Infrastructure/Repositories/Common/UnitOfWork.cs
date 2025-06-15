using Microsoft.EntityFrameworkCore.Storage;
using CQRS.Domain.Interfaces;
using CQRS.Infrastructure.Data;

namespace CQRS.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IEmployeeRepository? _employeeRepository;
    private IWeatherForecastRepository? _weatherForecastRepository;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEmployeeRepository Employees =>
        _employeeRepository ??= new EmployeeRepository(_context);

    public IWeatherForecastRepository WeatherForecasts =>
        _weatherForecastRepository ??= new WeatherForecastRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}