using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Db;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherStatementService
{
    private readonly IDataService<Assets> _assetsSource;
    private readonly IDataService<Sales> _salesSource;
    private readonly IDataService<Reviews> _reviewsSource;
    private readonly IDataService<Downloads> _downloadsSource;
    private readonly IStorage _storage;
    private readonly Month _month;

    public PublisherStatementService(
        IDataService<Assets> assetsSource,
        IDataService<Sales> salesSource,
        IDataService<Reviews> reviewsSource,
        IDataService<Downloads> downloadsSource,
        IStorage storage,
        Month month)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _storage = storage;
        _month = month;
    }

    public async Task<(PublisherStatement Refreshed, PublisherStatement Stored)> GetLatestAsync()
    {
        PublisherStatement[] statements = await Task.WhenAll(
            GetStoredAsync(),
            GetRefreshedAsync());

        return (Refreshed: statements.First(), Stored: statements.Last());
    }

    private async Task<PublisherStatement> GetStoredAsync()
    {
        PublisherStatement? storedStatementEntity = await _storage
            .GetValueOrDefaultAsync<PublisherStatement>(id: _month.Name);

        return storedStatementEntity ?? PublisherStatement.Empty();
    }

    public async Task<PublisherStatement> GetRefreshedAsync()
    {
        Task<Assets> getAssetsTask = _assetsSource.GetAsync();
        Task<Sales> getSalesTask = _salesSource.GetAsync();
        Task<Reviews> getrReviewsTask = _reviewsSource.GetAsync();
        Task<Downloads> getDownloadsTask = _downloadsSource.GetAsync();

        return PublisherStatement.Create(
            assets: await getAssetsTask,
            sales: await getSalesTask,
            reviews: await getrReviewsTask,
            downloads: await getDownloadsTask);
    }
}
