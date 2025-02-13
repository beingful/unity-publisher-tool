using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements;

public class StatementUpdateService : IDataSource<PublisherStatement>
{
    private readonly IDataSource<PublisherStatement> _storedStatementService;
    private readonly IDataSource<PublisherStatement> _refreshedStatementService;
    private readonly IStatementUpdateHandler _statementUpdateHandler;

    public StatementUpdateService(
        IDataSource<PublisherStatement> storedStatementService,
        IDataSource<PublisherStatement> refreshedStatementService,
        IStatementUpdateHandler statementUpdateHandler)
    {
        _storedStatementService = storedStatementService;
        _refreshedStatementService = refreshedStatementService;
        _statementUpdateHandler = statementUpdateHandler;
    }

    public async Task<PublisherStatement> GetAsync(CancellationToken cancellationToken = default)
    {
        PublisherStatement[] statements = await Task.WhenAll(
            _refreshedStatementService.GetAsync(cancellationToken),
            _storedStatementService.GetAsync(cancellationToken));

        return _statementUpdateHandler.Handle(
            newStatement: statements.First(),
            lastStatement: statements.Last());
    }
}
