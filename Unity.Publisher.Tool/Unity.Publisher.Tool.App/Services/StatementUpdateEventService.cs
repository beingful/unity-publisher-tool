using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateEventService : IDataService<PublisherStatement>
{
    private readonly PublisherStatementService _publisherStatementProvider;
    private readonly PublisherStatementHandler _publisherStatementHandler;
    private readonly IDataComparer<PublisherStatement> _statementComparer;

    public StatementUpdateEventService(
        PublisherStatementService publisherStatementProvider,
        PublisherStatementHandler publisherStatementHandler,
        IDataComparer<PublisherStatement> publisherEventComparer)
    {
        _publisherStatementProvider = publisherStatementProvider;
        _publisherStatementHandler = publisherStatementHandler;
        _statementComparer = publisherEventComparer;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        (PublisherStatement refreshedStatement, PublisherStatement storedStatement) =
            await _publisherStatementProvider.GetLatestAsync();

        PublisherStatement update;

        if (storedStatement.IsEmpty)
        {
            await _publisherStatementHandler.HandleNoStoredStatementAsync(refreshedStatement);

            update = PublisherStatement.Empty();
        }
        else if (_statementComparer.Different(refreshedStatement, storedStatement))
        {
            await _publisherStatementHandler.HandleStatementUpdatedAsync(refreshedStatement);

            update = _statementComparer.Difference(refreshedStatement, storedStatement);
        }
        else
        {
            update = PublisherStatement.Empty();
        }

        return update;
    }
}
