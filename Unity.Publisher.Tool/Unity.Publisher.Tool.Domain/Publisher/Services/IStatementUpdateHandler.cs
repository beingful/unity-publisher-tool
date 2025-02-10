namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public interface IStatementUpdateHandler
{
    public PublisherStatement Handle(PublisherStatement lastStatement, PublisherStatement newStatement);
}
