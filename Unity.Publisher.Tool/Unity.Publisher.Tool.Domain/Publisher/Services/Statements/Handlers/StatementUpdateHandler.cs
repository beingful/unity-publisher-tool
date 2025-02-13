using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public class StatementUpdateHandler : StatementUpdateBaseHandler
{
    public StatementUpdateHandler(
        IDataComparer<PublisherStatement> dataComparer, IDataStorage dataStorage)
        : base(new StatementUpdateStartingPointHandler(dataStorage,
            new StatementUpdateNewMonthHandler(
                new StatementUpdateDifferenceHandler(dataComparer, dataStorage))))
    {
    }
}
