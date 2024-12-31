namespace Unity.Publisher.Tool.Infrastructure.Db.Operations;

public interface IDbFunc
{
    Task<TOut> ExecuteAsync<TOut>();
}

public interface IDbFunc<TIn, TOut>
{
    Task<TOut?> ExecuteAsync(TIn input);
}
