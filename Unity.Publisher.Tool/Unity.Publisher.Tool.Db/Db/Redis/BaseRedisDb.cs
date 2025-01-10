using StackExchange.Redis;
using System.Text.Json;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

public abstract class BaseRedisDb<TDatabase> where TDatabase : IDatabaseAsync
{
    private readonly JsonSerializerOptions? _serializerOptions;

    private readonly static JsonSerializerOptions? _defaultSerializerOptions;

    static BaseRedisDb()
    {
        _defaultSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = true
        };
    }

    public BaseRedisDb(JsonSerializerOptions? jsonSerializerOptions = null)
    {
        _serializerOptions = jsonSerializerOptions ?? _defaultSerializerOptions;
    }

    protected abstract TDatabase Database { get; }

    public abstract StackExchange.Redis.ITransaction Transaction { get; }

    public async Task<TModel?> GetAsync<TModel>(RedisKey key)
    {
        TModel? result = default;

        string? data = await Database.StringGetAsync(key);

        if (string.IsNullOrEmpty(data) == false)
        {
            result = JsonSerializer.Deserialize<TModel>(data, _serializerOptions);
        }

        return result;
    }

    public Task SetAsync(RedisData data)
    {
        string value = JsonSerializer.Serialize(data.Value, _serializerOptions);

        return Database.StringSetAsync(data.Key, value, data.Lifetime, flags: CommandFlags.FireAndForget);
    }

    public Task RemoveAsync(RedisKey key)
    {
        return Database.KeyDeleteAsync(key);
    }
}
