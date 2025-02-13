using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public class StatementUpdateDifferenceHandler : StatementUpdateBaseHandler
{
    private readonly IDataComparer<PublisherStatement> _dataComparer;
    private readonly IDataStorage _dataStorage;

    public StatementUpdateDifferenceHandler(
        IDataComparer<PublisherStatement> dataComparer,
        IDataStorage dataStorage,
        IStatementUpdateHandler? next = null) : base(next)
    {
        _dataComparer = dataComparer;
        _dataStorage = dataStorage;
    }

    public override PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement)
    {
        PublisherStatement statementUpdate;

        if (_dataComparer.Different(newStatement, lastStatement))
        {
            _dataStorage.Set(newStatement);

            statementUpdate = _dataComparer.Difference(newStatement, lastStatement);
        }
        else
        {
            statementUpdate = base.Handle(lastStatement, newStatement);
        }

        return statementUpdate;
    }
}
