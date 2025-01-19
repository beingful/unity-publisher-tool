using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class PublisherFullStatementService : IPublisherRefreshedStatementService
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly IDataSource<Sales> _salesSource;
    private readonly IDataSource<Reviews> _reviewsSource;
    private readonly IDataSource<Downloads> _downloadsSource;
    private readonly DateTime _timestamp;

    public PublisherFullStatementService(
        IDataSource<Assets> assetsSource,
        IDataSource<Sales> salesSource,
        IDataSource<Reviews> reviewsSource,
        IDataSource<Downloads> downloadsSource,
        DateTime timestamp)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherStatement> GetAsync()
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
