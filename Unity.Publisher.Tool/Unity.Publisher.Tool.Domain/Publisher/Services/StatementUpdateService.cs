using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateService : IDataSource<PublisherStatement>
{
    private readonly IPublisherStoredStatementService _storedStatementService;
    private readonly IPublisherRefreshedStatementService _refreshedStatementService;
    private readonly IStatementUpdateHandler _statementUpdateHandler;

    public StatementUpdateService(
        IPublisherStoredStatementService storedStatementService,
        IPublisherRefreshedStatementService refreshedStatementService,
        IStatementUpdateHandler statementUpdateHandler)
    {
        _storedStatementService = storedStatementService;
        _refreshedStatementService = refreshedStatementService;
        _statementUpdateHandler = statementUpdateHandler;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        PublisherStatement[] statements = await Task.WhenAll(
            _storedStatementService.GetAsync(),
            _refreshedStatementService.GetAsync());

        return _statementUpdateHandler.Handle(
            lastStatement: statements.First(),
            newStatement: statements.Last());
    }
}
