using StackExchange.Redis;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

internal class RedisKeyProvider<TModel> : IProvider<Entity<TModel>, RedisKey>
{
    public RedisKey Provide(Entity<TModel> entity)
    {
        return new RedisKey(key: $"{typeof(TModel).Name}:{entity.Id}".ToLower());
    }
}
