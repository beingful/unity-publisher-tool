using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements;

public class StatementUpdateMessageService : PublisherMessageService<PublisherStatement>
{
    public StatementUpdateMessageService(
        IDataSource<PublisherStatement> statementUpdateSource,
        IPublisherDocumentExporter<PublisherStatement> statementUpdateExporter,
        ILogger<StatementUpdateMessageService> logger)
        : base(statementUpdateSource, statementUpdateExporter, logger)
    {
    }

    protected override bool CanMessage(PublisherStatement statementUpdate)
    {
        return statementUpdate.IsEmpty == false;
    }
}
