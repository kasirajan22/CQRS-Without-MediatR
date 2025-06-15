namespace CQRS.Application.Common.Interfaces;

public interface ICommand<TResult>
{
}

public interface ICommand : ICommand<bool>
{
}