using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;

public class PublisherPortalAction
{
    private readonly IHttpClient _httpClient;
    private readonly GetSalesPageEndpoint _unityPortalPageEndpoint;

    public PublisherPortalAction(IHttpClient httpClient)
    {
        _httpClient = httpClient;
        _unityPortalPageEndpoint = new GetSalesPageEndpoint();
    }

    public async Task<CallbackPageResult> RequestPageAsync(CancellationToken cancellationToken = default)
    {
        IHtmlHttpResponse bounceCallbackResponse = await _httpClient
            .GetAsync<IHtmlHttpResponse>(_unityPortalPageEndpoint.Path(), cancellationToken);

        return new CallbackPageResult(HtmlContent: bounceCallbackResponse.Content);
    }
}
