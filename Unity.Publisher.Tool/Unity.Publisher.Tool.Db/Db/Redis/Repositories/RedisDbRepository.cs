using StackExchange.Redis;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class RedisDbRepository : IStorage
{
    private readonly IStorage _baseRepository;

    public RedisDbRepository(ConnectionMultiplexer dbConnection)
    {
        _baseRepository = new BaseRedisDbRepository<IDatabaseAsync>(
            new RedisDb(dbConnection));
    }

    public ITransaction CreateTransaction()
    {
        return _baseRepository.CreateTransaction();
    }

    public async Task<TValue> GetAsync<TValue>(string id) where TValue : BaseEntity
    {
        return await _baseRepository.GetAsync<TValue>(id);
    }

    public async Task<TValue?> GetValueOrDefaultAsync<TValue>(string id) where TValue : BaseEntity
    {
        return await _baseRepository.GetValueOrDefaultAsync<TValue>(id);
    }

    public async Task UpdateAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        await _baseRepository.UpdateAsync(value);
    }

    public async Task UpsertAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        await _baseRepository.UpsertAsync(value);
    }

    public async Task InsertAsync<TValue>(TValue value) where TValue : BaseEntity
    {
        await _baseRepository.InsertAsync(value);
    }

    public async Task RemoveAsync<TValue>(string id) where TValue : BaseEntity
    {
        await _baseRepository.RemoveAsync<TValue>(id);
    }
}
