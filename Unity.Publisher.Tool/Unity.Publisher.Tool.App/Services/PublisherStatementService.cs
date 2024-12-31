using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public class PublisherStatementService
{
    private readonly IDataService<Assets> _assetsSource;
    private readonly IDataService<Sales> _salesSource;
    private readonly IDataService<Reviews> _reviewsSource;
    private readonly IDataService<Downloads> _downloadsSource;
    private readonly IPublisherEventDataStorage _publisherDataStorage;
    private readonly DateTime _now;

    public PublisherStatementService(
        IDataService<Assets> assetsSource,
        IDataService<Sales> salesSource,
        IDataService<Reviews> reviewsSource,
        IDataService<Downloads> downloadsSource,
        IPublisherEventDataStorage publisherDataStorage,
        DateTime now)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _publisherDataStorage = publisherDataStorage;
        _now = now;
    }

    public PublisherStatement GetStored()
    {
        PublisherStatement storedStatementEntity = _publisherDataStorage.Fetch<PublisherStatement>();

        return storedStatementEntity ?? PublisherStatement.Empty();
    }

    public async Task<PublisherStatement> RefreshAsync()
    {
        Task<Assets> getAssetsTask = _assetsSource.GetAsync();
        Task<Sales> getSalesTask = _salesSource.GetAsync();
        Task<Reviews> getrReviewsTask = _reviewsSource.GetAsync();
        Task<Downloads> getDownloadsTask = _downloadsSource.GetAsync();

        return PublisherStatement.Create(
            assets: await getAssetsTask,
            sales: await getSalesTask,
            reviews: await getrReviewsTask,
            downloads: await getDownloadsTask,
            creationTime: _now);
    }
}
