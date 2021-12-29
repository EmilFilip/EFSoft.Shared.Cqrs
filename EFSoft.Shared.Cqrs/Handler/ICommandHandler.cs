namespace EFSoft.Shared.Cqrs.Handler;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}