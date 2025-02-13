using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements;

public class PublisherDownloadlessStatementService : IDataSource<PublisherStatement>
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly ITimeDependentDataSource<Sales> _salesSource;
    private readonly ITimeDependentDataSource<Reviews> _reviewsSource;
    private readonly DateTime _timestamp;

    public PublisherDownloadlessStatementService(
        IDataSource<Assets> assetsSource,
        ITimeDependentDataSource<Sales> salesSource,
        ITimeDependentDataSource<Reviews> reviewsSource,
        DateTime timestamp)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherStatement> GetAsync(CancellationToken cancellationToken = default)
    {
        Task<Assets> getAssetsTask = _assetsSource.GetAsync(cancellationToken);
        Task<Sales> getSalesTask = _salesSource.GetAsync(_timestamp, cancellationToken);
        Task<Reviews> getrReviewsTask = _reviewsSource.GetAsync(_timestamp, cancellationToken);

        return PublisherStatement.Create(
            assets: await getAssetsTask,
            sales: await getSalesTask,
            reviews: await getrReviewsTask,
            downloads: Downloads.Empty(),
            creationTime: _timestamp);
    }
}
