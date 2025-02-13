using System.Runtime.CompilerServices;
using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Models.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi;

public sealed class PublisherReviewsApi : ITimeDependentDataSource<Reviews>
{
    private readonly PublisherApi _publisherApi;

    public PublisherReviewsApi(PublisherApi publisherApi)
    {
        _publisherApi = publisherApi;
    }

    public Task<Reviews> GetAsync(DateTime time, CancellationToken cancellationToken = default)
    {
        CancellationTokenSource cancellationTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        return GetReviewsAsync(time, cancellationTokenSource);
    }

    private async Task<Reviews> GetReviewsAsync(DateTime time, CancellationTokenSource cancellationTokenSource)
    {
        List<Review> targetReviews = [];

        await foreach (Reviews reviews in GetReviewsAsync(cancellationTokenSource.Token))
        {
            int targetReviewsCount = -1;

            if (reviews.IsEmpty == false)
            {
                targetReviewsCount = AddTargetReviews(reviews, targetReviews, time);
            }

            if (targetReviewsCount != reviews.Count)
            {
                await cancellationTokenSource.CancelAsync();

                cancellationTokenSource.Dispose();

                break;
            }
        }

        return new Reviews([.. targetReviews]);
    }

    private int AddTargetReviews(Reviews reviews, List<Review> targetReviews, DateTime time)
    {
        IEnumerable<Review> monthlyReviews = reviews.Collection.Where(x => x.Created.Month == time.Month);

        int newReviewsCount = monthlyReviews.Count();

        if (newReviewsCount > 0)
        {
            targetReviews.AddRange(monthlyReviews);
        }

        return newReviewsCount;
    }

    private async IAsyncEnumerable<Reviews> GetReviewsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (int i = 1; ;++i)
        {
            string endpoint = PublisherApiEndpoints.Reviews(_publisherApi.Publisher.Id, page: i);

            yield return await _publisherApi.GetAsync<GetReviewsResponse, Reviews>(endpoint, cancellationToken);
        }
    }
}
