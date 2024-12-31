using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public sealed class MonthlyReportEventService : IDataService<PublisherReport>
{
    private readonly PublisherStatementService _statementProvider;
    private readonly IDataService<PublisherInfo> _publisherInfoSource;
    private readonly IDataService<Revenue> _revenueSource;
    private readonly DateTime _now;

    public MonthlyReportEventService(
        PublisherStatementService statementProvider,
        IDataService<PublisherInfo> publisherInfoSource,
        IDataService<Revenue> revenueSource,
        DateTime now)
    {
        _statementProvider = statementProvider;
        _revenueSource = revenueSource;
        _publisherInfoSource = publisherInfoSource;
        _now = now;
    }

    public async Task<PublisherReport> GetAsync()
    {
        Task<PublisherInfo> getPublisherTask = _publisherInfoSource.GetAsync();
        Task<Revenue> getRevenueTask = _revenueSource.GetAsync();
        Task<PublisherStatement> getStatementTask = _statementProvider.RefreshAsync();

        return new PublisherReport(
            publisher: await getPublisherTask,
            revenue: await getRevenueTask,
            statement: await getStatementTask,
            month: new Month(order: _now.Month));
    }
}
