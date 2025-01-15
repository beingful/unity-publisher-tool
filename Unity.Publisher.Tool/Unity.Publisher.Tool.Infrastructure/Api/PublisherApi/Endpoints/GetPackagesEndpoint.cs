namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal sealed class GetPackagesEndpoint : ApiEndpoint
{
    public override string Path()
    {
        return "management/packages.json";
    }
}
