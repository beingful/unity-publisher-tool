using Unity.Publisher.Tool.Infrastructure.Db.Operations;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Operations;

public class RedisFetch<TData> : IDbFunc<string, TData>
{
    private readonly RedisDb _redisDb;
    //private readonly RedisKeyProvider<TData> _keyProvider;

    public RedisFetch(RedisDb redisDb)
    {
        _redisDb = redisDb;
        //_keyProvider = new RedisKeyProvider<TData>();
    }

    public async Task<TData?> ExecuteAsync(string id)
    {
        return await _redisDb.GetAsync<TData>(id);
    }
}
