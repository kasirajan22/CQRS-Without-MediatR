using Microsoft.AspNetCore.Mvc;
using CQRS.Application.Common.Interfaces;
using CQRS.Application.Features.Employees.Commands.CreateEmployee;
using CQRS.Application.Features.Employees.Commands.UpdateEmployee;
using CQRS.Application.Features.Employees.Commands.DeleteEmployee;
using CQRS.Application.Features.Employees.Queries.GetAllEmployees;
using CQRS.Application.Features.Employees.Queries.GetEmployeeById;
using CQRS.Application.Features.Employees.DTOs;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public EmployeeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    /// <summary>
    /// Get all employees
    /// </summary>
    /// <returns>List of employees</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
    {
        var query = new GetAllEmployeesQuery();
        var employees = await _queryDispatcher.DispatchAsync(query);
        return Ok(employees);
    }

    /// <summary>
    /// Get employee by ID
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <returns>Employee details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var query = new GetEmployeeByIdQuery { EmployeeId = id };
        var employee = await _queryDispatcher.DispatchAsync(query);
        
        if (employee == null)
            return NotFound($"Employee with ID {id} not found");
            
        return Ok(employee);
    }

    /// <summary>
    /// Create a new employee
    /// </summary>
    /// <param name="dto">Employee creation data</param>
    /// <returns>Created employee</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto dto)
    {
        var command = new CreateEmployeeCommand
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            DOB = dto.DOB,
            EmailID = dto.EmailID,
            PhoneNo = dto.PhoneNo,
            DOJ = dto.DOJ
        };

        var employee = await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeID }, employee);
    }

    /// <summary>
    /// Update an existing employee
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <param name="dto">Employee update data</param>
    /// <returns>Updated employee</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var command = new UpdateEmployeeCommand
        {
            EmployeeId = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            DOB = dto.DOB,
            EmailID = dto.EmailID,
            PhoneNo = dto.PhoneNo,
            DOJ = dto.DOJ
        };

        var employee = await _commandDispatcher.DispatchAsync(command);
        
        if (employee == null)
            return NotFound($"Employee with ID {id} not found");
            
        return Ok(employee);
    }

    /// <summary>
    /// Delete an employee
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        var command = new DeleteEmployeeCommand { EmployeeId = id };
        var success = await _commandDispatcher.DispatchAsync(command);
        
        if (!success)
            return NotFound($"Employee with ID {id} not found");
            
        return NoContent();
    }
}