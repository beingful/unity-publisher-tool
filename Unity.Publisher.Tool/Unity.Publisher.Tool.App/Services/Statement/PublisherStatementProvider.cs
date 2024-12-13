using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;

namespace Unity.Publisher.Tool.App.Services.Statement;

public class PublisherStatementProvider
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly IDataSource<Sales> _salesSource;
    private readonly IDataSource<Reviews> _reviewsSource;
    private readonly IDataSource<Downloads> _downloadsSource;
    private readonly IStorage _dbRepository;
    private readonly Month _month;

    public PublisherStatementProvider(
        IDataSource<Assets> assetsSource,
        IDataSource<Sales> salesSource,
        IDataSource<Reviews> reviewsSource,
        IDataSource<Downloads> downloadsSource,
        IStorage dbRepository,
        Month month)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _dbRepository = dbRepository;
        _month = month;
    }

    public async Task<(PublisherStatement Refreshed, PublisherStatement Stored)> ProvideLatestAsync()
    {
        PublisherStatement[] statements = await Task.WhenAll(
            FetchStoredAsync(),
            FetchRefreshedAsync());

        return (Refreshed: statements.First(), Stored: statements.Last());
    }

    private async Task<PublisherStatement> FetchStoredAsync()
    {
        PublisherStatementEntity? storedStatementEntity = await _dbRepository
            .GetValueOrDefaultAsync<PublisherStatementEntity>(id: _month.Name);

        return storedStatementEntity?.PublisherStatement ?? PublisherStatement.Empty();
    }

    public async Task<PublisherStatement> FetchRefreshedAsync()
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
