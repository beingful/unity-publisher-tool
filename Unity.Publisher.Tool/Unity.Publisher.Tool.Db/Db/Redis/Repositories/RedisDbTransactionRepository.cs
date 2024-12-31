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

    public async Task<TModel> GetAsync<TModel>(string id) where TModel : class
    {
        return await _baseRepository.GetAsync<TModel>(id);
    }

    public async Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class
    {
        return await _baseRepository.GetValueOrDefaultAsync<TModel>(id);
    }

    public async Task UpdateAsync<TModel>(Entity<TModel> value)
    {
        await _baseRepository.UpdateAsync(value);
    }

    public async Task UpsertAsync<TModel>(Entity<TModel> value)
    {
        await _baseRepository.UpsertAsync(value);
    }

    public async Task InsertAsync<TModel>(Entity<TModel> value)
    {
        await _baseRepository.InsertAsync(value);
    }

    public async Task RemoveAsync<TModel>(string id)
    {
        await _baseRepository.RemoveAsync<TModel>(id);
    }

    public async Task ExecuteAsync()
    {
        await _transaction.Transaction.ExecuteAsync();
    }
}
