using StackExchange.Redis;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class BaseRedisDbRepository<TOperationType> : IRedisStorage
    where TOperationType : IDatabaseAsync
{
    private readonly BaseRedisDb<TOperationType> _redisDb;

    public BaseRedisDbRepository(BaseRedisDb<TOperationType> redisDb)
    {
        _redisDb = redisDb;
    }

    public IRedisTransaction CreateTransaction()
    {
        return new RedisDbTransactionRepository(
            transaction: new RedisDbTransaction(_redisDb.Transaction));
    }

    public ITransactionQueue<IRedisTransaction, IRedisStorage> CreateTransactionQueue(IRedisTransaction transaction)
    {
        return new TransactionQueue<IRedisTransaction, IRedisStorage>(transaction);
    }

    public async Task<TModel> GetAsync<TModel>(string id) where TModel : class
    {
        TModel? result = await GetValueOrDefaultAsync<TModel>(id);

        return result!;
    }

    public Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class
    {
        RedisKey key = GetKey<TModel>(id);

        return _redisDb.GetAsync<TModel>(key);
    }

    public Task UpdateAsync<TModel>(Entity<TModel> entity)
    {
        return InsertAsync(entity);
    }

    public Task UpsertAsync<TModel>(Entity<TModel> entity)
    {
        return InsertAsync(entity);
    }

    public Task InsertAsync<TModel>(Entity<TModel> entity)
    {
        RedisKey key = GetKey<TModel>(entity.Id);

        return _redisDb.SetAsync(new RedisData(key, entity.Data!));
    }

    public Task RemoveAsync<TModel>(string id)
    {
        RedisKey key = GetKey<TModel>(id);

        return _redisDb.RemoveAsync(key);
    }

    private RedisKey GetKey<TModel>(string id)
    {
        return new RedisKey(key: $"{typeof(TModel).Name}:{id}".ToLower());
    }
}
