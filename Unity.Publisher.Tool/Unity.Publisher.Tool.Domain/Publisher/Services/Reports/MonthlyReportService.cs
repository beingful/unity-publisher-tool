using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Reports;

public sealed class MonthlyReportService : IDataSource<PublisherReport>
{
    private readonly IDataSource<PublisherStatement> _statementService;
    private readonly IDataSource<PublisherInfo> _publisherInfoSource;
    private readonly IDataSource<Revenue> _revenueSource;
    private readonly DateTime _timestamp;

    public MonthlyReportService(
        IDataSource<PublisherStatement> statementService,
        IDataSource<PublisherInfo> publisherInfoSource,
        IDataSource<Revenue> revenueSource,
        DateTime timestamp)
    {
        _statementService = statementService;
        _publisherInfoSource = publisherInfoSource;
        _revenueSource = revenueSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherReport> GetAsync(CancellationToken cancellationToken = default)
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync(cancellationToken);
        Task<Revenue> getRevenueForAllPreviousPeriodsTask = _revenueSource.GetAsync(cancellationToken);
        Task<PublisherStatement> getPublisherStatementTask = _statementService.GetAsync(cancellationToken);

        PublisherStatement publisherStatement = await getPublisherStatementTask;

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: CalculateTotalRevenue(
                latestRevenue: await getRevenueForAllPreviousPeriodsTask,
                assetsStatements: publisherStatement.AssetsStatements),
            statement: publisherStatement,
            month: new Month(order: _timestamp.Month));
    }

    private Revenue CalculateTotalRevenue(Revenue latestRevenue, AssetStatement[] assetsStatements)
    {
        decimal revenueFromSales = assetsStatements.Sum(asset => asset.Sales.Revenue);

        return new Revenue(total: latestRevenue.Total + revenueFromSales);
    }
}
