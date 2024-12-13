namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class GetSalesPageEndpoint : ApiEndpoint
{
    public override string Path()
    {
        return "https://publisher.assetstore.unity3d.com/sales.html";
    }
}
