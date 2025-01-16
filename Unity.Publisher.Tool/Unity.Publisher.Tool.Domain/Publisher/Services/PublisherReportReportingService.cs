using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherReportReportingService : PublisherReportingService<PublisherReport>
{
    public PublisherReportReportingService(
        IDataSource<PublisherReport> monthlyReportSource,
        PublisherDocumentExporter<PublisherReport> monthlyReportExporter,
        ILogger<PublisherReportReportingService> logger)
        : base(monthlyReportSource, monthlyReportExporter, logger)
    {
    }

    protected override bool CanReport(PublisherReport _)
    {
        return true;
    }
}
