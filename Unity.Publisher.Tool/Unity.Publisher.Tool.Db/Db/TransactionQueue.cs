namespace Unity.Publisher.Tool.Infrastructure.Db;

public class TransactionQueue<TTransaction, TRepository>
    : ITransactionQueue<TTransaction, TRepository>
    where TTransaction : ITransaction, TRepository
{
    private readonly TTransaction _transaction;

    public TransactionQueue(TTransaction transaction)
    {
        _transaction = transaction;
    }

    public ITransactionQueue<TTransaction, TRepository> Enqueue(Func<TRepository, Task> dbRequest)
    {
        dbRequest.Invoke(_transaction);

        return this;
    }

    public async Task ExecuteAsync()
    {
        await _transaction.ExecuteAsync();
    }
}
