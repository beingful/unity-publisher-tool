using StackExchange.Redis;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;
using Unity.Publisher.Tool.Infrastructure.Db.Redis.Models;

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

    public async Task<TValue> GetAsync<TValue>(string id) where TValue : BaseEntity
    {
        TValue? result = await GetValueOrDefaultAsync<TValue>(id);

        return result!;
    }

    public async Task<TValue?> GetValueOrDefaultAsync<TValue>(string id) where TValue : BaseEntity
    {
        string key = GetKey<TValue>(id);

        return await _redisDb.GetAsync<TValue>(key);
    }

    public async Task UpdateAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        await InsertAsync(value);
    }

    public async Task UpsertAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        await InsertAsync(value);
    }

    public async Task InsertAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        string key = GetKey<TValue>(value.Id);

        await _redisDb.SetAsync(new RedisData(key, value!));
    }

    public async Task RemoveAsync<TValue>(string id) where TValue : BaseEntity
    {
        string key = GetKey<TValue>(id);

        await _redisDb.RemoveAsync(key);
    }

    private string GetKey<TValue>(string id) where TValue : BaseEntity
    {
        return $"{typeof(TValue).Name.Replace("Entity", string.Empty)}:{id}".ToUpper();
    }
}
