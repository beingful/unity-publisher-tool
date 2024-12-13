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

    public async Task GetAsync(string? endpoint = null)
    {
        await _defaultHttpClient.GetAsync(endpoint);
    }

    public async Task<THttpResponse> GetAsync<THttpResponse>(string? endpoint)
        where THttpResponse : IHttpResponse
    {
        IFlurlRequest httpRequest = _defaultHttpClient
            .Request(endpoint)
            .SetMethod(HttpMethod.Get)
            .Build();

        return await SendAsync<THttpResponse>(httpRequest);
    }

    public async Task PostAsync(object? content = null, string? endpoint = null)
    {
        await _defaultHttpClient.PostAsync(content, endpoint);
    }

    public async Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(object content, string? endpoint)
        where THttpResponse : IHttpResponse
    {
        IFlurlRequest httpRequest = _defaultHttpClient
            .Request(endpoint)
            .WithUrlEncodedContent(content)
            .SetMethod(HttpMethod.Post)
            .Build();

        return await SendAsync<THttpResponse>(httpRequest);
    }

    private async Task<THttpResponse> SendAsync<THttpResponse>(IFlurlRequest httpRequest)
        where THttpResponse : IHttpResponse
    {
        TypedHttpClient typedHttpClient = _typedHttpClients[typeof(THttpResponse)];

        IHttpResponse httpResponse = await typedHttpClient.SendAsync(httpRequest);

        return (THttpResponse)httpResponse;
    }
}
