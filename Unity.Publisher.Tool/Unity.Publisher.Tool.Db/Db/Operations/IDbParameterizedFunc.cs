namespace Unity.Publisher.Tool.Infrastructure.Db.Operations;

public interface IDbParameterizedFunc
{
    Task<TOut?> ExecuteAsync<TIn, TOut>(TIn input);
}
