using System.Net;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients;

public interface IHttpClient
{
    Cookie? GetCookie(string name);

    Task GetAsync(string? endpoint = null);

    Task<THttpResponse> GetAsync<THttpResponse>(string? endpoint = null)
        where THttpResponse : IHttpResponse;

    Task PostAsync(object? content = null, string? endpoint = null);

    Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(object content, string? endpoint = null)
        where THttpResponse : IHttpResponse;
}

public interface IHttpClient<out TService> : IHttpClient;
