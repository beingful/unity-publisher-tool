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

    public Task GetAsync(string? endpoint)
    {
        return _httpClient.GetAsync(endpoint);
    }

    public Task<THttpResponse> GetAsync<THttpResponse>(string? endpoint)
        where THttpResponse : IHttpResponse
    {
        return _httpClient.GetAsync<THttpResponse>(endpoint);
    }

    public Task PostAsync(object? content, string? endpoint)
    {
        return _httpClient.PostAsync(endpoint);
    }

    public Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(object content, string? endpoint)
        where THttpResponse : IHttpResponse
    {
        return _httpClient.PostUrlEncodedAsync<THttpResponse>(content, endpoint);
    }
}
