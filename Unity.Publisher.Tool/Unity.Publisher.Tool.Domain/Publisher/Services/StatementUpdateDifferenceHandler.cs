using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

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

    public override PublisherStatement Handle(PublisherStatement lastStatement, PublisherStatement newStatement)
    {
        PublisherStatement statementUpdate;

        if (_dataComparer.Different(lastStatement, newStatement))
        {
            _dataStorage.Set(newStatement);

            statementUpdate = _dataComparer.Difference(lastStatement, newStatement);
        }
        else
        {
            statementUpdate = base.Handle(lastStatement, newStatement);
        }

        return statementUpdate;
    }
}
