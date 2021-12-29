namespace EFSoft.Shared.Cqrs.Handler;

public interface IQueryHandler<in TParameters, TResult>
    where TParameters : IQueryParameters
    where TResult : IQueryResult
{
    Task<TResult> HandleAsync(
        TParameters parameters,
        CancellationToken ct = default);
}