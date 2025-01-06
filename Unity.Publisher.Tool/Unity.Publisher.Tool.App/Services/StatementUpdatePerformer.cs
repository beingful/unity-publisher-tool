using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdatePerformer : PublisherEventNotificationPerformer
{
    public StatementUpdatePerformer(
        PublisherStatementReportingService statementUpdateReportingService,
        ILogger<StatementUpdatePerformer> logger)
        : base(PublisherEvent.StatementUpdate, statementUpdateReportingService, logger)
    {
    }
}
