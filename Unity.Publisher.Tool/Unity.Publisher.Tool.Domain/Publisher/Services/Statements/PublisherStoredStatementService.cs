using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements;

public class PublisherStoredStatementService : IDataSource<PublisherStatement>
{
    private readonly IDataStorage _dataStorage;

    public PublisherStoredStatementService(IDataStorage storage)
    {
        _dataStorage = storage;
    }

    public Task<PublisherStatement> GetAsync(CancellationToken cancellationToken = default)
    {
        PublisherStatement storedStatementEntity =
            _dataStorage.Fetch<PublisherStatement>() ?? PublisherStatement.Empty();

        return Task.FromResult(storedStatementEntity);
    }
}
