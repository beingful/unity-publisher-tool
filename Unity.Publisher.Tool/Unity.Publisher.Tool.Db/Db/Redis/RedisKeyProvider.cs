using StackExchange.Redis;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

internal class RedisKeyProvider<TModel>
{
    public RedisKey Provide(Entity<TModel> entity)
    {
        return new RedisKey(key: $"{typeof(TModel).Name}:{entity.Id}".ToLower());
    }
}
