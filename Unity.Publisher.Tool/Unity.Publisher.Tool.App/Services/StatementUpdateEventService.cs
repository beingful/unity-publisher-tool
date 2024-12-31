using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateEventService : IDataService<PublisherStatement>
{
    private readonly PublisherStatementService _publisherStatementService;
    private readonly IPublisherEventDataStorage _publisherEventDataStorage;
    private readonly IDataComparer<PublisherStatement> _statementComparer;

    public StatementUpdateEventService(
        PublisherStatementService publisherStatementService,
        IPublisherEventDataStorage publisherEventDataStorage,
        IDataComparer<PublisherStatement> statementComparer)
    {
        _publisherStatementService = publisherStatementService;
        _publisherEventDataStorage = publisherEventDataStorage;
        _statementComparer = statementComparer;
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
            _publisherEventDataStorage.Set(refreshedStatement);

            update = PublisherStatement.Empty();
        }
        else if (_statementComparer.Different(refreshedStatement, storedStatement))
        {
            _publisherEventDataStorage.Set(refreshedStatement);

            update = _statementComparer.Difference(refreshedStatement, storedStatement);
        }
        else
        {
            update = PublisherStatement.Empty();
        }

        return update;
    }
}
