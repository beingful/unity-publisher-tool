using System.Text.Json;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

public class RedisDbTransaction : BaseRedisDb<StackExchange.Redis.ITransaction>
{
    private readonly StackExchange.Redis.ITransaction _transaction;

    public RedisDbTransaction(StackExchange.Redis.ITransaction transaction, JsonSerializerOptions? jsonSerializerOptions = null)
        : base(jsonSerializerOptions)
    {
        _transaction = transaction;
    }

    protected override StackExchange.Redis.ITransaction Database => _transaction;

    public override StackExchange.Redis.ITransaction Transaction => _transaction;
}
