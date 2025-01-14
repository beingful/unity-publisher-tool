namespace Unity.Publisher.Tool.Infrastructure.Db;

public class Entity<TModel>
{
    public required string Id { get; init; }

    public TModel? Data { get; init; }
}
