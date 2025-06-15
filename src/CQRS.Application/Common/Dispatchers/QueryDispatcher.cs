using Microsoft.Extensions.DependencyInjection;
using CQRS.Application.Common.Interfaces;

namespace CQRS.Application.Common.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        
        try
        {
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("HandleAsync");
            if (method == null)
                throw new InvalidOperationException($"HandleAsync method not found for query {query.GetType().Name}");

            var result = method.Invoke(handler, new object[] { query });
            if (result is Task<TResult> task)
                return await task;

            throw new InvalidOperationException($"Handler for query {query.GetType().Name} did not return expected type");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("No service for type"))
        {
            throw new InvalidOperationException($"No handler registered for query {query.GetType().Name}. Handler type: {handlerType.Name}", ex);
        }
    }
}