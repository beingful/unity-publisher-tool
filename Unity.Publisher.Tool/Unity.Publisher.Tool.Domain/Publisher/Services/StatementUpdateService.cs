using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateService : IDataSource<PublisherStatement>
{
    private readonly PublisherStatementService _publisherStatementService;
    private readonly IDataComparer<PublisherStatement> _publisherStatementComparer;
    private readonly IDataStorage _dataStorage;

    public StatementUpdateService(
        PublisherStatementService publisherStatementService,
        IDataComparer<PublisherStatement> publisherStatementComparer,
        IDataStorage dataStorage)
    {
        _publisherStatementService = publisherStatementService;
        _publisherStatementComparer = publisherStatementComparer;
        _dataStorage = dataStorage;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        PublisherStatement storedStatement = _publisherStatementService
            .GetStored();

        PublisherStatement refreshedStatement = await _publisherStatementService
            .RefreshAsync();

        PublisherStatement update;

        if (storedStatement.IsEmpty)
        {
            _dataStorage.Set(refreshedStatement);

            update = PublisherStatement.Empty();
        }
        else if (_publisherStatementComparer.Different(refreshedStatement, storedStatement))
        {
            _dataStorage.Set(refreshedStatement);

            update = _publisherStatementComparer.Difference(refreshedStatement, storedStatement);
        }
        else
        {
            update = PublisherStatement.Empty();
        }

        return update;
    }
}
