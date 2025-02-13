using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherSalesApi : ITimeDependentDataSource<Sales>
{
    private readonly PublisherApi _publisherApi;

    public PublisherSalesApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public Task<Sales> GetAsync(DateTime time, CancellationToken cancellationToken = default)
    {
        string endpoint = PublisherApiEndpoints.Sales(_publisherApi.Publisher.Id, time);

        return _publisherApi.GetAsync<GetSalesResponse, Sales>(endpoint, cancellationToken);
    }
}
