using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateService : IDataSource<PublisherStatement>
{
    private readonly IPublisherStoredStatementService _storedStatementService;
    private readonly IPublisherRefreshedStatementService _refreshedStatementService;
    private readonly IDataComparer<PublisherStatement> _statementsComparer;
    private readonly IDataStorage _dataStorage;

    public StatementUpdateService(
        IPublisherStoredStatementService storedStatementService,
        IPublisherRefreshedStatementService refreshedStatementService,
        IDataComparer<PublisherStatement> statementsComparer,
        IDataStorage dataStorage)
    {
        _storedStatementService = storedStatementService;
        _refreshedStatementService = refreshedStatementService;
        _statementsComparer = statementsComparer;
        _dataStorage = dataStorage;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        PublisherStatement[] statements = await Task.WhenAll(
            _storedStatementService.GetAsync(),
            _refreshedStatementService.GetAsync());

        return GetStatementUpdate(
            storedStatement: statements.First(),
            refreshedStatement: statements.Last());
    }

    private PublisherStatement GetStatementUpdate(PublisherStatement storedStatement, PublisherStatement refreshedStatement)
    {
        PublisherStatement statementUpdate;

        if (storedStatement.IsEmpty)
        {
            _dataStorage.Set(refreshedStatement);

            statementUpdate = PublisherStatement.Empty();
        }
        else if (_statementsComparer.Different(refreshedStatement, storedStatement))
        {
            _dataStorage.Set(refreshedStatement);

            statementUpdate = _statementsComparer.Difference(refreshedStatement, storedStatement);
        }
        else
        {
            statementUpdate = PublisherStatement.Empty();
        }

        return statementUpdate;
    }
}
