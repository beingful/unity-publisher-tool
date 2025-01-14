using StackExchange.Redis;
using System.Text.Json;

namespace Unity.Publisher.Tool.Infrastructure.Db.Redis;

public class RedisDb : BaseRedisDb<IDatabaseAsync>
{
    private readonly ConnectionMultiplexer _dbConnection;

    public RedisDb(ConnectionMultiplexer dbConnection, JsonSerializerOptions? jsonSerializerOptions = null)
        : base(jsonSerializerOptions)
    {
        _dbConnection = dbConnection;
    }

    protected override IDatabaseAsync Database => _dbConnection.GetDatabase();

    public override StackExchange.Redis.ITransaction Transaction => _dbConnection.GetDatabase().CreateTransaction();
}
