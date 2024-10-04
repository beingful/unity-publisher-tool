using Newtonsoft.Json;

namespace Unity.Publisher.Tool.Http;

public abstract class ApiService;

public abstract class ApiService<TApiService> : ApiService
    where TApiService : ApiService
{
    private readonly HttpClient _httpClient;
    protected readonly ILogger Logger;

    public ApiService(IConfiguration configuration, ILogger<TApiService> logger, JsonSerializerSettings? serializerSettings = default)
    {
        _httpClient = new HttpClient(
            baseUrl: GetApiUrl(configuration),
            serializerSettings: serializerSettings);

        Logger = logger;
    }

    public async Task PostAsync(object request, string? endpoint = null,
        params (string Name, string Value)[] headers)
    {
        HttpRequest httpRequest = _httpClient.CreateRequest(endpoint);

        httpRequest.AddHeaders(headers);

        await httpRequest.PostWithContentAsync(request);
    }

    private static string GetApiUrl(IConfiguration configuration)
    {
        string apiName = typeof(TApiService).Name
            .Replace(nameof(ApiService), string.Empty);

        return configuration.GetConnectionString(apiName)!;
    }
}
