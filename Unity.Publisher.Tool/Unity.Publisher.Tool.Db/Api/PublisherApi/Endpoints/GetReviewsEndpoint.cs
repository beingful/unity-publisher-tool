namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetReviewsEndpoint : PublisherEndpoint
{
    public GetReviewsEndpoint(long publisherId) : base(publisherId)
    {
    }

    public override string Path()
    {
        return WithPulisherId("publisher-info/reviews/{0}.json");
    }
}
