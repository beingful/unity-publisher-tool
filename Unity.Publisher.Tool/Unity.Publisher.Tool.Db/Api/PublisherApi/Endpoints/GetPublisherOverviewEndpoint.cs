namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetPublisherOverviewEndpoint : PublisherEndpoint
{
    public GetPublisherOverviewEndpoint(long publisherId) : base(publisherId)
    {
    }

    public override string Path()
    {
        return WithPulisherId("publisher/overview.json");
    }
}
