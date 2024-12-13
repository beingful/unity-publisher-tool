using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Infrastructure.Db.Entities;

public class PublisherStatementEntity : BaseEntity
{
    public override required string Id { get; init; }

    public required PublisherStatement PublisherStatement { get; init; }
}
