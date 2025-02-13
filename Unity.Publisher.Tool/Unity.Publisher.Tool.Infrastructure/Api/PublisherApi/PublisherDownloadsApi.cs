using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherDownloadsApi : ITimeDependentDataSource<Downloads>
{
    private readonly PublisherApi _publisherApi;

    public PublisherDownloadsApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public Task<Downloads> GetAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        string endpoint = PublisherApiEndpoints.Downloads(_publisherApi.Publisher.Id, date);

        return _publisherApi.GetAsync<GetDownloadsResponse, Downloads>(endpoint, cancellationToken);
    }
}
