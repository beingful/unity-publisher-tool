namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class RedisDbTransactionRepository : IRedisTransaction
{
    private readonly RedisDbTransaction _transaction;
    private readonly IRedisStorage _baseRepository;

    public RedisDbTransactionRepository(RedisDbTransaction transaction)
    {
        _transaction = transaction;
        _baseRepository = new BaseRedisDbRepository<StackExchange.Redis.ITransaction>(transaction);
    }

    public IRedisTransaction CreateTransaction()
    {
        return this;
    }

    public ITransactionQueue<IRedisTransaction, IRedisStorage> CreateTransactionQueue(IRedisTransaction transaction)
    {
        return _baseRepository.CreateTransactionQueue(transaction);
    }

    public Task<TModel> GetAsync<TModel>(string id) where TModel : class
    {
        return _baseRepository.GetAsync<TModel>(id);
    }

    public Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class
    {
        return _baseRepository.GetValueOrDefaultAsync<TModel>(id);
    }

    public Task UpdateAsync<TModel>(Entity<TModel> value)
    {
        return _baseRepository.UpdateAsync(value);
    }

    public Task UpsertAsync<TModel>(Entity<TModel> value)
    {
        return _baseRepository.UpsertAsync(value);
    }

    public Task InsertAsync<TModel>(Entity<TModel> value)
    {
        return _baseRepository.InsertAsync(value);
    }

    public Task RemoveAsync<TModel>(string id)
    {
        return _baseRepository.RemoveAsync<TModel>(id);
    }

    public Task ExecuteAsync()
    {
        return _transaction.Transaction.ExecuteAsync();
    }
}
