using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherRevenueApi : IDataSource<Revenue>
{
    private readonly PublisherApi _publisherApi;

    public PublisherRevenueApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public Task<Revenue> GetAsync(CancellationToken cancellationToken = default)
    {
        string endpoint = PublisherApiEndpoints.Revenue(_publisherApi.Publisher.Id);

        return _publisherApi.GetAsync<GetRevenueResponse, Revenue>(endpoint, cancellationToken);
    }
}
