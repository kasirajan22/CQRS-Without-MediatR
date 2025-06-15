using Microsoft.Extensions.DependencyInjection;
using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Common.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        
        try
        {
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("HandleAsync");
            if (method == null)
                throw new InvalidOperationException($"HandleAsync method not found for command {command.GetType().Name}");

            var result = method.Invoke(handler, new object[] { command });
            if (result is Task<TResult> task)
                return await task;

            throw new InvalidOperationException($"Handler for command {command.GetType().Name} did not return expected type");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("No service for type"))
        {
            throw new InvalidOperationException($"No handler registered for command {command.GetType().Name}. Handler type: {handlerType.Name}", ex);
        }
    }

    public async Task<bool> DispatchAsync(ICommand command)
    {
        return await DispatchAsync<bool>(command);
    }
}