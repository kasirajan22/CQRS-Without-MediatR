namespace CQRS.Application.Common.Interfaces;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, bool> where TCommand : ICommand
{
}