namespace EFSoft.Shared.Cqrs.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}