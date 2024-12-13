using Flurl.Http;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;
using Unity.Publisher.Tool.Infrastructure.Http.Responses.Flurl;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public class HtmlHttpClient : TypedHttpClient
{
    public HtmlHttpClient(IFlurlClient flurlClient) : base(flurlClient)
    {
    }

    public override async Task<IHttpResponse> SendAsync(IFlurlRequest httpRequest)
    {
        IFlurlResponse httpResponse = await SendFlurlAsync(httpRequest);

        return new HtmlHttpResponse(httpResponse);
    }
}
