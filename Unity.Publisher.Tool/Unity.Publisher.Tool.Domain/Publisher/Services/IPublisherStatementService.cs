namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public interface IPublisherStatementService
{
    Task<PublisherStatement> GetAsync();
}
