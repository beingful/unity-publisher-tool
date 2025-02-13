namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public interface IStatementUpdateHandler
{
    public PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement);
}
