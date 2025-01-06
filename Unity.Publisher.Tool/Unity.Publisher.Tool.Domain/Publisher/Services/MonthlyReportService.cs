using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public sealed class MonthlyReportService : IDataSource<PublisherReport>
{
    private readonly PublisherStatementService _publisherStatementService;
    private readonly IDataSource<PublisherInfo> _publisherInfoSource;
    private readonly IDataSource<Revenue> _revenueSource;
    private readonly DateTime _timestamp;

    public MonthlyReportService(
        PublisherStatementService publisherStatementProvider,
        IDataSource<PublisherInfo> publisherInfoSource,
        IDataSource<Revenue> revenueSource,
        DateTime timestamp)
    {
        _publisherStatementService = publisherStatementProvider;
        _publisherInfoSource = publisherInfoSource;
        _revenueSource = revenueSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherReport> GetAsync()
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync();
        Task<Revenue> getRevenueTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getStatementTask = _publisherStatementService.RefreshAsync();

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: await getRevenueTask,
            statement: await getStatementTask,
            month: new Month(order: _timestamp.Month));
    }
}
