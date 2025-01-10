using System.Net;
using Unity.Publisher.Tool.Infrastructure.Http.Responses;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients;

public interface IHttpClient
{
    Cookie? GetCookie(string name);

    Task GetAsync(string? endpoint = null, CancellationToken cancellationToken = default);

    Task<THttpResponse> GetAsync<THttpResponse>(string? endpoint = null, CancellationToken cancellationToken = default)
        where THttpResponse : IHttpResponse;

    Task PostAsync(object? content = null, string? endpoint = null, CancellationToken cancellationToken = default);

    Task<THttpResponse> PostUrlEncodedAsync<THttpResponse>(object content, string? endpoint = null, CancellationToken cancellationToken = default)
        where THttpResponse : IHttpResponse;
}

public interface IHttpClient<out TService> : IHttpClient;
