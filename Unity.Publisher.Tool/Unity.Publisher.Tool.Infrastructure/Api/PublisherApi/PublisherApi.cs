using Autofac;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
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
    private readonly DateTime _timestamp;

    public PublisherApi(
        IHttpClient<PublisherApi> httpClient,
        ISessionManager<PublisherApi> sessionManager,
        ILogger<PublisherApi> logger,
        DateTime now) : base(httpClient, sessionManager, logger)
    {
        _timestamp = now;
    }

    internal static PublisherProfile? Publisher { get; private set; }

    void IStartable.Start()
    {
        Publisher = GetPublisherIdAsync().Result;
    }

    Task<Assets> IDataSource<Assets>.GetAsync()
    {
        GetPackagesEndpoint packagesEndpoint = new();

        return GetAsync<GetPackagesResponse, Assets>(packagesEndpoint.Path());
    }

    Task<Sales> IDataSource<Sales>.GetAsync()
    {
        GetSalesEndpoint salesEndpoint = new(Publisher!.Id, _timestamp);

        return GetAsync<GetSalesResponse, Sales>(salesEndpoint.Path());
    }

    async Task<Reviews> IDataSource<Reviews>.GetAsync()
    {
        List<Review> reviewsCollection = [];

        using CancellationTokenSource cancellationTokenSource = new();

        await foreach (Reviews reviews in GetReviewsAsync().WithCancellation(cancellationTokenSource.Token))
        {
            IEnumerable<Review> targetReviews = reviews.Collection
                .Where(x => x.Created.Month == _timestamp.Month);

            int targetReviewsCount = targetReviews.Count();

            if (targetReviewsCount != 0)
            {
                reviewsCollection.AddRange(targetReviews);
            }

            if ((reviews.Collection.Length != targetReviewsCount) || reviews.IsEmpty)
            {
                await cancellationTokenSource.CancelAsync();

                break;
            }
        }

        return new Reviews([..reviewsCollection]);
    }

    private async IAsyncEnumerable<Reviews> GetReviewsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (int i = 1; ; ++i)
        {
            GetReviewsEndpoint reviewsEndpoint = new(Publisher!.Id, targetPage: i);

            yield return await GetAsync<GetReviewsResponse, Reviews>(
                reviewsEndpoint.Path(), cancellationToken);
        }
    }

    Task<Downloads> IDataSource<Downloads>.GetAsync()
    {
        GetDownloadsEndpoint downloadsEndpoint = new(Publisher!.Id, _timestamp);

        return GetAsync<GetDownloadsResponse, Downloads>(downloadsEndpoint.Path());
    }

    Task<Revenue> IDataSource<Revenue>.GetAsync()
    {
        GetRevenueEndpoint revenueEndpoint = new(Publisher!.Id);

        return GetAsync<GetRevenueResponse, Revenue>(revenueEndpoint.Path());
    }

    async Task<PublisherInfo> IDataSource<PublisherInfo>.GetAsync()
    {
        GetPublisherOverviewEndpoint publisherOverviewEndpoint = new(Publisher!.Id);

        Rating publisherRating = await GetAsync<GetPublisherOverviewResponse, Rating>(
            publisherOverviewEndpoint.Path());

        return new PublisherInfo(Publisher.Id, Publisher.Name, publisherRating);
    }

    private Task<PublisherProfile> GetPublisherIdAsync()
    {
        GetUserOverviewEndpoint userOverviewEndpoint = new();

        return GetAsync<GetUserOverviewResponse, PublisherProfile>(
            userOverviewEndpoint.Path());
    }
}
