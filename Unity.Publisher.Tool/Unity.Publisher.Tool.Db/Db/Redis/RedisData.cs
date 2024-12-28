using StackExchange.Redis;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

public class RedisData
{
    public readonly RedisKey Key;
    public readonly object Value;
    public readonly TimeSpan? Lifetime;

    public RedisData(RedisKey key, object value, TimeSpan? lifetime = null)
    {
        Key = key;
        Value = value;
        Lifetime = lifetime;
    }
}
