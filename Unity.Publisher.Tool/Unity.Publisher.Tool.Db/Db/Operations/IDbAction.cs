namespace Unity.Publisher.Tool.Infrastructure.Db.Operations;

public interface IDbAction
{
    Task ExecuteAsync();
}

public interface IDbAction<TIn>
{
    Task ExecuteAsync(TIn input);
}

public interface IDbAction<TFirstIn, TSecondIn>
{
    Task ExecuteAsync(TFirstIn firstInput, TSecondIn secondInput);
}
