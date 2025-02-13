using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public class StatementUpdateStartingPointHandler : StatementUpdateBaseHandler
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

        if (IsBaseline(lastStatement, newStatement))
        {
            _dataStorage.Set(newStatement);

            statementUpdate = PublisherStatement.Empty();
        }
        else
        {
            statementUpdate = base.Handle(lastStatement, newStatement);
        }

        return statementUpdate;
    }

    private bool IsBaseline(PublisherStatement lastStatement, PublisherStatement newStatement)
    {
        return lastStatement.IsEmpty
            || lastStatement.CreationTime.Month != newStatement.CreationTime.Month;
    }
}
