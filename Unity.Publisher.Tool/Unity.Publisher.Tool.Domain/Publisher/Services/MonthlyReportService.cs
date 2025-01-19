using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public sealed class MonthlyReportService : IDataSource<PublisherReport>
{
    private readonly IPublisherStatementService _statementService;
    private readonly IDataSource<PublisherInfo> _publisherInfoSource;
    private readonly IDataSource<Revenue> _revenueSource;
    private readonly DateTime _timestamp;

    public MonthlyReportService(
        IPublisherStatementService statementService,
        IDataSource<PublisherInfo> publisherInfoSource,
        IDataSource<Revenue> revenueSource,
        DateTime timestamp)
    {
        _statementService = statementService;
        _publisherInfoSource = publisherInfoSource;
        _revenueSource = revenueSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherReport> GetAsync()
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync();
        Task<Revenue> getRevenueForAllPreviousPeriodsTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getPublisherStatementTask = _statementService.GetAsync();

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
