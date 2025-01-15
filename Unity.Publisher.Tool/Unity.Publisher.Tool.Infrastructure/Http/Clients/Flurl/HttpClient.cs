using Flurl.Http;
using System.Net;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl;

public class HttpClient : IHttpClient
{
    private readonly DefaultHttpClient _defaultHttpClient;
    private readonly IReadOnlyDictionary<Type, TypedHttpClient> _typedHttpClients;

    public HttpClient(string? baseUrl = null)
    {
        FlurlClient flurlClient = new(baseUrl);

        _defaultHttpClient = new DefaultHttpClient(flurlClient);

        _typedHttpClients = new Dictionary<Type, TypedHttpClient>
        {
            { typeof(IHtmlHttpResponse), new HtmlHttpClient(flurlClient) },
            { typeof(IJsonHttpResponse), new JsonHttpClient(flurlClient) }
        };
    }

    public Cookie? GetCookie(string name)
    {
        return _defaultHttpClient.GetCookie(name);
    }

    public Task GetAsync(string? endpoint = null, CancellationToken cancellationToken = default)
    {
        return _defaultHttpClient.GetAsync(endpoint, cancellationToken);
    }

    public Task<THttpResponse> GetAsync<THttpResponse>(string? endpoint,
        CancellationToken cancellationToken = default)
        where THttpResponse : IHttpResponse
    {
        IFlurlRequest httpRequest = _defaultHttpClient
            .Request(endpoint)
            .SetMethod(HttpMethod.Get)
            .Build();

        return SendAsync<THttpResponse>(httpRequest, cancellationToken);
    }

    public Task PostAsync(object? content = null, string? endpoint = null,
        CancellationToken cancellationToken = default)
    {
        return _defaultHttpClient.PostAsync(content, endpoint, cancellationToken);
    }

    public Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(
        object content, string? endpoint, CancellationToken cancellationToken = default) where THttpResponse : IHttpResponse
    {
        IFlurlRequest httpRequest = _defaultHttpClient
            .Request(endpoint)
            .WithUrlEncodedContent(content)
            .SetMethod(HttpMethod.Post)
            .Build();

        return SendAsync<THttpResponse>(httpRequest, cancellationToken);
    }

    private async Task<THttpResponse> SendAsync<THttpResponse>(
        IFlurlRequest httpRequest,
        CancellationToken cancellationToken) where THttpResponse : IHttpResponse
    {
        TypedHttpClient typedHttpClient = _typedHttpClients[typeof(THttpResponse)];

        IHttpResponse httpResponse = await typedHttpClient.SendAsync(httpRequest, cancellationToken);

        return (THttpResponse)httpResponse;
    }
}
