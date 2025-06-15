namespace CQRS.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendWelcomeEmailAsync(string to, string firstName, string lastName);
    Task SendEmployeeCreatedNotificationAsync(string to, string employeeName);
}