namespace Unity.Publisher.Tool.Infrastructure.Db.Operations;

public interface IDbParameterizedAction
{
    Task ExecuteAsync<TIn>(TIn input);
}
