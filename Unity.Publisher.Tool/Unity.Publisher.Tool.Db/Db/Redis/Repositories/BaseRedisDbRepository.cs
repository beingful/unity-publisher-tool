using StackExchange.Redis;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class BaseRedisDbRepository<TOperationType> : IStorage
    where TOperationType : IDatabaseAsync
{
    private readonly BaseRedisDb<TOperationType> _redisDb;

    public BaseRedisDbRepository(BaseRedisDb<TOperationType> redisDb)
    {
        _redisDb = redisDb;
    }

    public ITransaction CreateTransaction()
    {
        return new RedisDbTransactionRepository(new RedisDbTransaction(_redisDb.Transaction));
    }

    public async Task<TModel> GetAsync<TModel>(string id) where TModel : class
    {
        TModel? result = await GetValueOrDefaultAsync<TModel>(id);

        return result!;
    }

    public async Task<TModel?> GetValueOrDefaultAsync<TModel>(string id) where TModel : class
    {
        RedisKey key = GetKey<TModel>(id);

        return await _redisDb.GetAsync<TModel>(key);
    }

    public async Task UpdateAsync<TModel>(Entity<TModel> entity)
    {
        await InsertAsync(entity);
    }

    public async Task UpsertAsync<TModel>(Entity<TModel> entity)
    {
        await InsertAsync(entity);
    }

    public async Task InsertAsync<TModel>(Entity<TModel> entity)
    {
        RedisKey key = GetKey<TModel>(entity.Id);

        await _redisDb.SetAsync(new RedisData(key, entity.Data!));
    }

    public async Task RemoveAsync<TModel>(string id)
    {
        RedisKey key = GetKey<TModel>(id);

        await _redisDb.RemoveAsync(key);
    }

    private RedisKey GetKey<TModel>(string id)
    {
        return new RedisKey(key: $"{typeof(TModel).Name}:{id}".ToLower());
    }
}
