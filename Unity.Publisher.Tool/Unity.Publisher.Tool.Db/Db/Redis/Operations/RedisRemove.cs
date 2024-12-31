using Unity.Publisher.Tool.Infrastructure.Db.Operations;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Operations;

public class RedisRemove<TData> : IDbAction<string>
{
    private readonly RedisDb _redisDb;
    //private readonly RedisKeyProvider<TData> _keyProvider;

    public RedisRemove(RedisDb redisDb)
    {
        _redisDb = redisDb;
        //_keyProvider = new RedisKeyProvider<TData>();
    }

    public async Task ExecuteAsync(string id)
    {
        await _redisDb.RemoveAsync(key: id);
    }
}
