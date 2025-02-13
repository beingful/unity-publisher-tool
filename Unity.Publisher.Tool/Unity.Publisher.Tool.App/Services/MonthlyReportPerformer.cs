using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services;

namespace Unity.Publisher.Tool.App.Services;

public class MonthlyReportPerformer : PublisherEventNotificationPerformer
{
    public MonthlyReportPerformer(
        PublisherMessageService<PublisherReport> messageService,
        ILogger<MonthlyReportPerformer> logger)
        : base(PublisherEvent.MonthlyReport, messageService, logger)
    {
    }
}
