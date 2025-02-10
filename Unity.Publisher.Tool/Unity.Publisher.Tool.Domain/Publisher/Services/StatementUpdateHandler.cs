using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateHandler : StatementUpdateBaseHandler
{
    public StatementUpdateHandler(
        IDataComparer<PublisherStatement> dataComparer, IDataStorage dataStorage)
        : base(new StatementUpdateBaselineHandler(
            dataStorage, new StatementUpdateDifferenceHandler(dataComparer, dataStorage)))
    {
    }
}
