namespace Unity.Publisher.Tool.Infrastructure.Db;

public abstract class Entity
{
    public required string Id { get; init; }
}

public class Entity<TModel>
{
    public required string Id { get; init; }

    public required TModel Data { get; init; }
}
