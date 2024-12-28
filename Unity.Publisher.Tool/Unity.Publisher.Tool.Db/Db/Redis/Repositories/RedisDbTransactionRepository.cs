namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class RedisDbTransactionRepository : ITransaction
{
    private readonly RedisDbTransaction _transaction;
    private readonly IStorage _baseRepository;

    public RedisDbTransactionRepository(RedisDbTransaction transaction)
    {
        _transaction = transaction;
        _baseRepository = new BaseRedisDbRepository<StackExchange.Redis.ITransaction>(transaction);
    }

    public ITransaction Enqueue(Func<IStorage, Task> dbRequest)
    {
        dbRequest.Invoke(_baseRepository);

        return this;
    }

    public async Task ExecuteAsync()
    {
        await _transaction.Transaction.ExecuteAsync();
    }
}
