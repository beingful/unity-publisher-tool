using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherStatementService
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly IDataSource<Sales> _salesSource;
    private readonly IDataSource<Reviews> _reviewsSource;
    private readonly IDataSource<Downloads> _downloadsSource;
    private readonly IDataStorage _dataStorage;
    private readonly DateTime _timestamp;

    public PublisherStatementService(
        IDataSource<Assets> assetsSource,
        IDataSource<Sales> salesSource,
        IDataSource<Reviews> reviewsSource,
        IDataSource<Downloads> downloadsSource,
        IDataStorage storage,
        DateTime timestamp)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _dataStorage = storage;
        _timestamp = timestamp;
    }

    public PublisherStatement GetStored()
    {
        PublisherStatement? storedStatementEntity = _dataStorage.Fetch<PublisherStatement>();

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
            creationTime: _timestamp);
    }
}
