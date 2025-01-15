using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services;

namespace Unity.Publisher.Tool.App.Services;

public class MonthlyReportPerformer : PublisherEventNotificationPerformer
{
    public MonthlyReportPerformer(
        PublisherReportReportingService monthlyReportReportingService,
        ILogger<MonthlyReportPerformer> logger)
        : base(PublisherEvent.MonthlyReport, monthlyReportReportingService, logger)
    {
    }
}
