namespace EFSoft.Shared.Cqrs.Command;

public interface ICommandExecutorWithResult
{
    Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default)
            where TCommand : ICommand
            where TResult : ICommandResult;
}