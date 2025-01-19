using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherStoredStatementService : IPublisherStoredStatementService
{
    private readonly IDataStorage _dataStorage;

    public PublisherStoredStatementService(IDataStorage storage)
    {
        _dataStorage = storage;
    }

    public Task<PublisherStatement> GetAsync()
    {
        PublisherStatement storedStatementEntity =
            _dataStorage.Fetch<PublisherStatement>() ?? PublisherStatement.Empty();

        return Task.FromResult(storedStatementEntity);
    }
}
