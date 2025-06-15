using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Common.Behaviors;

public class ValidationBehavior<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _handler;

    public ValidationBehavior(ICommandHandler<TCommand, TResult> handler)
    {
        _handler = handler;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        // Add validation logic here if needed
        // For now, just pass through to the actual handler
        return await _handler.HandleAsync(command);
    }
}