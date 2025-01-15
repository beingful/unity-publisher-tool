namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface ITransactionQueue<TTransaction, TRepository>
    where TTransaction : ITransaction, TRepository
{
    ITransactionQueue<TTransaction, TRepository> Enqueue(Func<TRepository, Task> dbRequest);

    Task ExecuteAsync();
}
