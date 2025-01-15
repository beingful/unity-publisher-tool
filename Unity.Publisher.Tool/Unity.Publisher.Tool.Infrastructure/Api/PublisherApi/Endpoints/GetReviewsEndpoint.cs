namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetReviewsEndpoint : PublisherEndpoint
{
    public GetReviewsEndpoint(long publisherId, int targetPage, int reviewsPerPage = 1) : base(publisherId)
    {
    }

    public override string Path()
    {
        return WithPulisherId("publisher-info/reviews/{0}.json?page=1&rows=2&order_key=date&sort=desc");
    }
}
