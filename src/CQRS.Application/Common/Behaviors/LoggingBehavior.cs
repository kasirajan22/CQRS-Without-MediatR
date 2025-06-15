using Microsoft.Extensions.Logging;
using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Common.Behaviors;

public class LoggingBehavior<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _handler;
    private readonly ILogger<LoggingBehavior<TCommand, TResult>> _logger;

    public LoggingBehavior(ICommandHandler<TCommand, TResult> handler, ILogger<LoggingBehavior<TCommand, TResult>> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        var commandName = typeof(TCommand).Name;
        
        _logger.LogInformation("Handling command: {CommandName}", commandName);
        
        try
        {
            var result = await _handler.HandleAsync(command);
            _logger.LogInformation("Successfully handled command: {CommandName}", commandName);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling command: {CommandName}", commandName);
            throw;
        }
    }
}