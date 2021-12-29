namespace EFSoft.Shared.Cqrs.Command;

public interface ICommandExecutor
{
    Task ExecuteAsync<TCommand>(TCommand command)
        where TCommand : ICommand;
}