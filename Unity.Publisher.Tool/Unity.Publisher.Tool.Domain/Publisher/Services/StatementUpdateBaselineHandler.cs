using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateBaselineHandler : StatementUpdateBaseHandler
{
    private readonly IDataStorage _dataStorage;

    public StatementUpdateBaselineHandler(
        IDataStorage dataStorage,
        IStatementUpdateHandler? next = null) : base(next)
    {
        _dataStorage = dataStorage;
    }

    public override PublisherStatement Handle(PublisherStatement lastStatement, PublisherStatement newStatement)
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
