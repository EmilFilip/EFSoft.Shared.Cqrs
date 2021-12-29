namespace EFSoft.Shared.Cqrs.Query;

public interface IQueryExecutor
{
    Task<TResult> ExecuteAsync<TParameters, TResult>(
        TParameters parameters, 
        CancellationToken ct = default)
            where TParameters : IQueryParameters
            where TResult : IQueryResult;
}