namespace CQRS.Domain.Events;

public class EmployeeUpdatedEvent
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public EmployeeUpdatedEvent(int employeeId, string firstName, string lastName, string emailId, DateTime updatedAt)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        EmailID = emailId;
        UpdatedAt = updatedAt;
    }
}