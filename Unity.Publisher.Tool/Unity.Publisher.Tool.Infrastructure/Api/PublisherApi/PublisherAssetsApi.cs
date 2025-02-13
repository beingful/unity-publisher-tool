using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherAssetsApi : IDataSource<Assets>
{
    private readonly PublisherApi _publisherApi;

    public PublisherAssetsApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public Task<Assets> GetAsync(CancellationToken cancellationToken = default)
    {
        string endpoint = PublisherApiEndpoints.Packages();

        return _publisherApi.GetAsync<GetPackagesResponse, Assets>(endpoint, cancellationToken);
    }
}
