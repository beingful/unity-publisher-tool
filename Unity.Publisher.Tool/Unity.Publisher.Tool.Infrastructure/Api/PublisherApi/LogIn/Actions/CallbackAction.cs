using Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;
using Unity.Publisher.Tool.Infrastructure.Http.Clients;
using Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Actions;

public class CallbackAction
{
    protected readonly IHttpClient HttpClient;

    public CallbackAction(IHttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public Task RedirectAsync(CallbackPageResult callbackPage, CancellationToken cancellationToken = default)
    {
        string? callbackUrl = FetchCallBackUrl(callbackPage.HtmlContent);

        if (string.IsNullOrWhiteSpace(callbackUrl))
        {
            throw new MissingMemberException($"A callback url is missing.");
        }

        return HttpClient.GetAsync(callbackUrl, cancellationToken);
    }

    private string? FetchCallBackUrl(IHtmlContent htmlContent)
    {
        return htmlContent
            .SearchDocument()
            .FilterBy()
            .Tag("a")
            .BuildFilter()
            .FetchAttributeValue(targetAttribute: "href");
    }
}
