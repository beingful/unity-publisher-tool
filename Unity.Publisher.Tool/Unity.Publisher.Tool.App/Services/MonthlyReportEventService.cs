using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public sealed class MonthlyReportEventService : IDataService<PublisherReport>
{
    private readonly PublisherStatementService _statementProvider;
    private readonly IDataService<PublisherInfo> _publisherInfoSource;
    private readonly IDataService<Revenue> _revenueSource;
    private readonly Month _currentMonth;

    public MonthlyReportEventService(
        PublisherStatementService statementProvider,
        IDataService<PublisherInfo> publisherInfoSource,
        IDataService<Revenue> revenueSource,
        Month currentMonth)
    {
        _statementProvider = statementProvider;
        _revenueSource = revenueSource;
        _publisherInfoSource = publisherInfoSource;
        _currentMonth = currentMonth;
    }

    public async Task<PublisherReport> GetAsync()
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync();
        Task<Revenue> getRevenueTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getStatementTask = _statementProvider.GetRefreshedAsync();

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: await getRevenueTask,
            statement: await getStatementTask,
            month: _currentMonth);
    }
}
