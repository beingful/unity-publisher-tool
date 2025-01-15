namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetLogInPageEndpoint : ApiEndpoint
{
    public override string Path()
    {
        return "https://id.unity.com/auth/genesis_login";
    }
}
