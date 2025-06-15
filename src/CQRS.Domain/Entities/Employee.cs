using CQRS.Domain.Entities.Common;
using CQRS.Domain.Enums;

namespace CQRS.Domain.Entities;

public class Employee : BaseEntity
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DOB { get; set; }
    public string EmailID { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public DateOnly DOJ { get; set; }

    // Computed properties
    public string FullName => $"{FirstName} {LastName}";
    public int Age => DateTime.Now.Year - DOB.Year;
    public int YearsOfService => DateTime.Now.Year - DOJ.Year;
}