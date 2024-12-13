using Autofac;
using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;
using Unity.Publisher.Tool.Infrastructure.Api.State;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public class PublisherApi : StatefulApi, IStartable,
    IDataSource<PublisherInfo>,
    IDataSource<Revenue>,
    IDataSource<Assets>,
    IDataSource<Sales>,
    IDataSource<Reviews>,
    IDataSource<Downloads>
{
    private readonly Month _currentMonth;

    public PublisherApi(
        Month currentMonth,
        IHttpClient<PublisherApi> httpClient,
        ISessionManager<PublisherApi> sessionManager,
        ILogger<PublisherApi> logger) : base(httpClient, sessionManager, logger)
    {
        _currentMonth = currentMonth;
    }

    internal static PublisherProfile Publisher { get; private set; }

    void IStartable.Start()
    {
        Publisher = GetPublisherIdAsync().Result;
    }

    async Task<Assets> IDataSource<Assets>.GetAsync()
    {
        GetPackagesEndpoint packagesEndpoint = new();

        return await GetAsync<GetPackagesResponse, Assets>(packagesEndpoint.Path());
    }

    async Task<Sales> IDataSource<Sales>.GetAsync()
    {
        GetSalesEndpoint salesEndpoint = new(Publisher.Id);

        return await GetAsync<GetSalesResponse, Sales>(salesEndpoint.Path());
    }

    async Task<Reviews> IDataSource<Reviews>.GetAsync()
    {
        GetReviewsEndpoint reviewsEndpoint = new(Publisher.Id);

        Reviews reviews = await GetAsync<GetReviewsResponse, Reviews>(reviewsEndpoint.Path());

        return new Reviews(reviews.Collection
            .Where(x => x.Created.Month == _currentMonth.Order)
            .ToArray());
    }

    async Task<Downloads> IDataSource<Downloads>.GetAsync()
    {
        GetDownloadsEndpoint downloadsEndpoint = new(Publisher.Id);

        string path = downloadsEndpoint.Path();

        return await GetAsync<GetDownloadsResponse, Downloads>(downloadsEndpoint.Path());
    }

    async Task<Revenue> IDataSource<Revenue>.GetAsync()
    {
        GetRevenueEndpoint revenueEndpoint = new(Publisher.Id);

        return await GetAsync<GetRevenueResponse, Revenue>(revenueEndpoint.Path());
    }

    async Task<PublisherInfo> IDataSource<PublisherInfo>.GetAsync()
    {
        GetPublisherOverviewEndpoint publisherOverviewEndpoint = new(Publisher.Id);

        Rating publisherRating = await GetAsync<GetPublisherOverviewResponse, Rating>(
            publisherOverviewEndpoint.Path());

        return new PublisherInfo(Publisher.Id, Publisher.Name, publisherRating);
    }

    private async Task<PublisherProfile> GetPublisherIdAsync()
    {
        GetUserOverviewEndpoint userOverviewEndpoint = new();

        GetUserOverviewResponse userOverviewResponse =
            await GetAsync<GetUserOverviewResponse>(userOverviewEndpoint.Path());

        return userOverviewResponse.Convert();
    }
}
