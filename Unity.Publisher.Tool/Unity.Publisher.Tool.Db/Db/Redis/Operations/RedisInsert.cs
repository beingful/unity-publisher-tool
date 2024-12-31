using Unity.Publisher.Tool.Infrastructure.Db.Operations;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Operations;

public class RedisInsert<TData> : IDbAction<string, TData> where TData : notnull
{
    private readonly RedisDb _redisDb;
    //private readonly RedisKeyProvider<TData> _keyProvider;

    public RedisInsert(RedisDb redisDb)
    {
        _redisDb = redisDb;
        //_keyProvider = new RedisKeyProvider<TData>();
    }

    public async Task ExecuteAsync(string id, TData data)
    {
        RedisData redisData = new(key: id, value: data);

        await _redisDb.SetAsync(redisData);
    }
}
