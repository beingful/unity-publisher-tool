namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetUserOverviewEndpoint : ApiEndpoint
{
    public override string Path()
    {
        return "/user/overview.json";
    }
}
