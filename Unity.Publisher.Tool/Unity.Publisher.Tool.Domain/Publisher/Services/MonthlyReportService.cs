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
        Task<Revenue> getRevenueForAllPreviousPeriodsTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getPublisherStatementTask = _publisherStatementService.RefreshAsync();

        Revenue latestRevenue = await getRevenueForAllPreviousPeriodsTask;
        PublisherStatement publisherStatement = await getPublisherStatementTask;

        decimal revenueFromSales = publisherStatement.AssetsStatements
            .Sum(asset => asset.Sales.Revenue);

        Revenue totalRevenue = new(total: latestRevenue.Total + revenueFromSales);

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: totalRevenue,
            statement: publisherStatement,
            month: new Month(order: _timestamp.Month));
    }
}
