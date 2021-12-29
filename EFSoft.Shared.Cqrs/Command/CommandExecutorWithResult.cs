namespace EFSoft.Shared.Cqrs.Command;

public class CommandExecutorWithResult : ICommandExecutorWithResult
{
    private readonly IServiceProvider _container;

    public CommandExecutorWithResult(
        IServiceProvider container)
    {
        _container = container
            ?? throw new ArgumentNullException(nameof(container));
    }

    public async Task<TResult> ExecuteAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default)
            where TCommand : ICommand
            where TResult : ICommandResult
    {
        ct.ThrowIfCancellationRequested();

        var handler = _container.GetRequiredService<ICommandHandlerWithResult<TCommand, TResult>>();

        return await handler.HandleAsync(command, ct);
    }
}