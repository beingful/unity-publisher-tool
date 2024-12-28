using StackExchange.Redis;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Repositories;

public class RedisDbRepository : IStorage
{
    private readonly IStorage _baseRepository;

    public RedisDbRepository(ConnectionMultiplexer dbConnection)
    {
        _baseRepository = new BaseRedisDbRepository<IDatabaseAsync>(
            redisDb: new RedisDb(dbConnection));
    }

    public ITransaction CreateTransaction()
    {
        return _baseRepository.CreateTransaction();
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
}
