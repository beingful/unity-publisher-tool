using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

internal class StatementUpdateStartingPointHandler : StatementUpdateBaseHandler
{
    private readonly IDataStorage _dataStorage;

    public StatementUpdateStartingPointHandler(
        IDataStorage dataStorage,
        IStatementUpdateHandler? next = null) : base(next)
    {
        _dataStorage = dataStorage;
    }

    public override PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement)
    {
        PublisherStatement statementUpdate;

        if (lastStatement.IsEmpty)
        {
            _dataStorage.Set(newStatement);

            statementUpdate = PublisherStatement.Empty();
        }
        else
        {
            statementUpdate = base.Handle(newStatement, lastStatement);
        }

        return statementUpdate;
    }
}
