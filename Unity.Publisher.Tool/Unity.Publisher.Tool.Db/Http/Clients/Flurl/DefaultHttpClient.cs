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

    public async Task GetAsync(string? endpoint)
    {
        HttpRequestBuilder httpRequest = Request(endpoint).SetMethod(HttpMethod.Get);

        await SendAsync(httpRequest);
    }

    public async Task PostAsync(object? content, string? endpoint)
    {
        HttpRequestBuilder httpRequest = Request(endpoint)
            .WithJsonContent(content)
            .SetMethod(HttpMethod.Post);

        await SendAsync(httpRequest);
    }

    private async Task SendAsync(HttpRequestBuilder httpRequest)
    {
        await _flurlClient.SendAsync(httpRequest.Build(), HttpCompletionOption.ResponseHeadersRead);
    }
}
