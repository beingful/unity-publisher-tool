using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherDownloadlessStatementService : IPublisherRefreshedStatementService
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly IDataSource<Sales> _salesSource;
    private readonly IDataSource<Reviews> _reviewsSource;
    private readonly DateTime _timestamp;

    public PublisherDownloadlessStatementService(
        IDataSource<Assets> assetsSource,
        IDataSource<Sales> salesSource,
        IDataSource<Reviews> reviewsSource,
        DateTime timestamp)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        Task<Assets> getAssetsTask = _assetsSource.GetAsync();
        Task<Sales> getSalesTask = _salesSource.GetAsync();
        Task<Reviews> getrReviewsTask = _reviewsSource.GetAsync();

        return PublisherStatement.Create(
            assets: await getAssetsTask,
            sales: await getSalesTask,
            reviews: await getrReviewsTask,
            downloads: Downloads.Empty(),
            creationTime: _timestamp);
    }
}
