using System.Net;
using Flurl.Http;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public class DefaultHttpClient
{
    private static CookieJar _cookieJar = new();

    private readonly IFlurlClient _flurlClient;

    public DefaultHttpClient(IFlurlClient flurlClient)
    {
        _flurlClient = flurlClient;
    }

    public Cookie? GetCookie(string name)
    {
        FlurlCookie? cookie = _cookieJar?.FirstOrDefault(cookie =>
        {
            return cookie.Name == name;
        });

        return cookie == null
            ? null
            : new Cookie(name: cookie.Name, value: cookie.Value)
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires?.DateTime ?? DateTime.MinValue
            };
    }

    public HttpRequestBuilder Request(string? endpoint = null)
    {
        HttpRequestBuilder request = new(_flurlClient.Request(endpoint));

        return request.WithCookies(_cookieJar);
    }

    public Task GetAsync(string? endpoint, CancellationToken cancellationToken = default)
    {
        HttpRequestBuilder httpRequest = Request(endpoint).SetMethod(HttpMethod.Get);

        return SendAsync(httpRequest, cancellationToken);
    }

    public Task PostAsync(object? content, string? endpoint, CancellationToken cancellationToken = default)
    {
        HttpRequestBuilder httpRequest = Request(endpoint)
            .WithJsonContent(content)
            .SetMethod(HttpMethod.Post);

        return SendAsync(httpRequest, cancellationToken);
    }

    private Task SendAsync(HttpRequestBuilder httpRequest, CancellationToken cancellationToken)
    {
        return _flurlClient.SendAsync(httpRequest.Build(), HttpCompletionOption.ResponseHeadersRead, cancellationToken);
    }
}
