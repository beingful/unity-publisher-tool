using Microsoft.Extensions.Configuration;
using HttpClient = Unity.Publisher.Tool.Infrastructure.Http.Clients.Flurl.HttpClient;

namespace Unity.Publisher.Tool.Infrastructure.Http.Clients;

public class HttpClientFactory : IHttpClientFactory
{
    private readonly IConfiguration _configuration;

    public HttpClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IHttpClient CreateFor<TService>()
    {
        return new HttpClient(baseUrl: GetBaseUrl<TService>());
    }

    private string? GetBaseUrl<TService>()
    {
        string apiName = typeof(TService).Name;

        return _configuration.GetConnectionString(apiName);
    }
}
