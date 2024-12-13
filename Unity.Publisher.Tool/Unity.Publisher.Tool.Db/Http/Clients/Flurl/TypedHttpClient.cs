using Flurl.Http;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public abstract class TypedHttpClient
{
    private readonly IFlurlClient _flurlClient;

    public TypedHttpClient(IFlurlClient flurlClient)
    {
        _flurlClient = flurlClient;
    }

    public abstract Task<IHttpResponse> SendAsync(IFlurlRequest httpRequest);

    protected async Task<IFlurlResponse> SendFlurlAsync(IFlurlRequest httpRequest)
    {
        return await _flurlClient.SendAsync(httpRequest);
    }
}
