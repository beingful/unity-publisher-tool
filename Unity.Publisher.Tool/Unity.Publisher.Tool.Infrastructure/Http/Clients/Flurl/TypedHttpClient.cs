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

    public abstract Task<IHttpResponse> SendAsync(IFlurlRequest httpRequest, CancellationToken cancellationToken = default);

    protected Task<IFlurlResponse> SendFlurlAsync(IFlurlRequest httpRequest, CancellationToken cancellationToken)
    {
        return _flurlClient.SendAsync(httpRequest, cancellationToken: cancellationToken);
    }
}
