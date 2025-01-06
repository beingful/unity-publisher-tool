using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherReportReportingService : PublisherReportingService<PublisherReport>
{
    public PublisherReportReportingService(
        IDataSource<PublisherReport> monthlyReportSource,
        PublisherDocumentExporter<PublisherReport> monthlyReportExporter)
        : base(monthlyReportSource, monthlyReportExporter)
    {
    }

    protected override bool CanReport(PublisherReport _)
    {
        return true;
    }
}
