using Microsoft.Extensions.Logging;
using CQRS.Application.Services;

namespace CQRS.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // For demo purposes, just log the email
        _logger.LogInformation("Sending email to {To} with subject: {Subject}", to, subject);
        _logger.LogDebug("Email body: {Body}", body);
        
        // In a real implementation, you would integrate with an email service like SendGrid, SMTP, etc.
        await Task.Delay(100); // Simulate async operation
    }

    public async Task SendWelcomeEmailAsync(string to, string firstName, string lastName)
    {
        var subject = "Welcome to the Company!";
        var body = $"Dear {firstName} {lastName},\n\nWelcome to our company! We're excited to have you on board.\n\nBest regards,\nHR Team";
        
        await SendEmailAsync(to, subject, body);
    }

    public async Task SendEmployeeCreatedNotificationAsync(string to, string employeeName)
    {
        var subject = "New Employee Added";
        var body = $"A new employee '{employeeName}' has been added to the system.";
        
        await SendEmailAsync(to, subject, body);
    }
}