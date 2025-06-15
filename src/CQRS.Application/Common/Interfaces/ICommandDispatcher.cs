namespace CQRS.Application.Common.Interfaces;

public interface ICommandDispatcher
{
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command);
    Task<bool> DispatchAsync(ICommand command);
}