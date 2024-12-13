using Unity.Publisher.Tool.App.Services.Statement;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services.Report;

public sealed class MonthlyReportProvider : IEventDataProvider<PublisherReport>
{
    private readonly PublisherStatementProvider _statementProvider;
    private readonly IDataSource<PublisherInfo> _publisherInfoSource;
    private readonly IDataSource<Revenue> _revenueSource;
    private readonly Month _currentMonth;

    public MonthlyReportProvider(
        PublisherStatementProvider statementProvider,
        IDataSource<PublisherInfo> publisherInfoSource,
        IDataSource<Revenue> revenueSource,
        Month currentMonth)
    {
        _statementProvider = statementProvider;
        _revenueSource = revenueSource;
        _publisherInfoSource = publisherInfoSource;
        _currentMonth = currentMonth;
    }

    public async Task<PublisherReport> ProvideAsync()
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync();
        Task<Revenue> getRevenueTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getStatementTask = _statementProvider.FetchRefreshedAsync();

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: await getRevenueTask,
            statement: await getStatementTask,
            month: _currentMonth);
    }
}
