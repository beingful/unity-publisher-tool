using System.Net;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients;

public class DynamicHttpClient<TService> : IHttpClient<TService>
{
    private readonly IHttpClient _httpClient;

    public DynamicHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateFor<TService>();
    }

    public Cookie? GetCookie(string name)
    {
        return _httpClient.GetCookie(name);
    }

    public Task GetAsync(string? endpoint, CancellationToken cancellationToken = default)
    {
        return _httpClient.GetAsync(endpoint, cancellationToken);
    }

    public Task<THttpResponse> GetAsync<THttpResponse>(
        string? endpoint, CancellationToken cancellationToken = default)
        where THttpResponse : IHttpResponse
    {
        return _httpClient.GetAsync<THttpResponse>(endpoint, cancellationToken);
    }

    public Task PostAsync(object? content, string? endpoint, CancellationToken cancellationToken = default)
    {
        return _httpClient.PostAsync(endpoint, cancellationToken: cancellationToken);
    }

    public Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(
        object content, string? endpoint, CancellationToken cancellationToken = default)
        where THttpResponse : IHttpResponse
    {
        return _httpClient.PostUrlEncodedAsync<THttpResponse>(content, endpoint, cancellationToken);
    }
}
