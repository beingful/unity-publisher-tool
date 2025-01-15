using Flurl.Http;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;
using Unity.Publisher.Tool.Infrastructure.Http.Responses.Flurl;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public class JsonHttpClient : TypedHttpClient
{
    public JsonHttpClient(IFlurlClient flurlClient) : base(flurlClient)
    {
    }

    public override async Task<IHttpResponse> SendAsync(IFlurlRequest httpRequest, CancellationToken cancellationToken)
    {
        IFlurlResponse httpResponse = await SendFlurlAsync(httpRequest, cancellationToken);

        return new JsonHttpResponse(httpResponse);
    }
}