namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetRevenueEndpoint : PublisherEndpoint
{
    public GetRevenueEndpoint(long publisherId) : base(publisherId)
    {
    }

    public override string Path()
    {
        return WithPulisherId("publisher-info/revenue/{0}.json");
    }
}
