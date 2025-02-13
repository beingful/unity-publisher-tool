using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements;

public class PublisherFullStatementService : IDataSource<PublisherStatement>
{
    private readonly IDataSource<Assets> _assetsSource;
    private readonly ITimeDependentDataSource<Sales> _salesSource;
    private readonly ITimeDependentDataSource<Reviews> _reviewsSource;
    private readonly ITimeDependentDataSource<Downloads> _downloadsSource;
    private readonly DateTime _timestamp;

    public PublisherFullStatementService(
        IDataSource<Assets> assetsSource,
        ITimeDependentDataSource<Sales> salesSource,
        ITimeDependentDataSource<Reviews> reviewsSource,
        ITimeDependentDataSource<Downloads> downloadsSource,
        DateTime timestamp)
    {
        _assetsSource = assetsSource;
        _salesSource = salesSource;
        _reviewsSource = reviewsSource;
        _downloadsSource = downloadsSource;
        _timestamp = timestamp;
    }

    public async Task<PublisherStatement> GetAsync(CancellationToken cancellationToken = default)
    {
        Task<Assets> getAssetsTask = _assetsSource.GetAsync(cancellationToken);
        Task<Sales> getSalesTask = _salesSource.GetAsync(_timestamp, cancellationToken);
        Task<Reviews> getrReviewsTask = _reviewsSource.GetAsync(_timestamp, cancellationToken);
        Task<Downloads> getDownloadsTask = _downloadsSource.GetAsync(_timestamp, cancellationToken);

        return PublisherStatement.Create(
            assets: await getAssetsTask,
            sales: await getSalesTask,
            reviews: await getrReviewsTask,
            downloads: await getDownloadsTask,
            creationTime: _timestamp);
    }
}
