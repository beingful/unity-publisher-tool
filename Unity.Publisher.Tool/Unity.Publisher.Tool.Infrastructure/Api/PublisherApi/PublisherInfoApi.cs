using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherInfoApi : IDataSource<PublisherInfo>
{
    private readonly PublisherApi _publisherApi;

    public PublisherInfoApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public async Task<PublisherInfo> GetAsync(CancellationToken cancellationToken = default)
    {
        string endpoint = PublisherApiEndpoints.PublisherInfo();

        Rating publisherRating = await _publisherApi
            .GetAsync<GetPublisherOverviewResponse, Rating>(endpoint, cancellationToken);

        return new PublisherInfo(_publisherApi.Publisher.Id, _publisherApi.Publisher.Name, publisherRating);
    }
}
