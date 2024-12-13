namespace Unity.Publisher.Tool.Infrastructure.Db.Redis.Models;

public class RedisData
{
    public string Key;
    public object Value;
    public TimeSpan? Lifetime;

    public RedisData(string key, object value, TimeSpan? lifetime = null)
    {
        Key = key;
        Value = value;
        Lifetime = lifetime;
    }
}
