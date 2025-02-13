using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;
using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;

public class PublisherPortalAction
{
    private readonly IHttpClient _httpClient;

    public PublisherPortalAction(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CallbackPageResult> RequestPageAsync(CancellationToken cancellationToken = default)
    {
        IHtmlHttpResponse bounceCallbackResponse = await _httpClient
            .GetAsync<IHtmlHttpResponse>(PublisherApiEndpoints.SalesPage(), cancellationToken);

        return new CallbackPageResult(HtmlContent: bounceCallbackResponse.Content);
    }
}
