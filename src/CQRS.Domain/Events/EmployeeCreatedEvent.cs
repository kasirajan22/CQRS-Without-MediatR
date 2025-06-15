namespace CQRS.Domain.Events;

public class EmployeeCreatedEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public EmployeeCreatedEvent(int employeeId, string firstName, string lastName, string emailId, DateTime createdAt)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        EmailID = emailId;
        CreatedAt = createdAt;
    }
}