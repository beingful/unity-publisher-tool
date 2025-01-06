using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherStatementReportingService : PublisherReportingService<PublisherStatement>
{
    public PublisherStatementReportingService(
        IDataSource<PublisherStatement> statementUpdateSource,
        PublisherDocumentExporter<PublisherStatement> statementUpdateExporter)
        : base(statementUpdateSource, statementUpdateExporter)
    {
    }

    protected override bool CanReport(PublisherStatement statementUpdate)
    {
        return statementUpdate.IsEmpty == false;
    }
}
