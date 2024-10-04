using Flurl.Http;
using Flurl.Http.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Unity.Publisher.Tool.Http;

public sealed class HttpClient
{
    private readonly FlurlClient _flurlClient;
    public static readonly JsonSerializerSettings DefaultSerializerSettings;

    static HttpClient()
    {
        DefaultSerializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Converters = [new StringEnumConverter()]
        };
    }

    public HttpClient(string baseUrl, JsonSerializerSettings? serializerSettings = default)
    {
        _flurlClient = new(baseUrl);

        _flurlClient.WithSettings(settings =>
        {
            settings.JsonSerializer = new NewtonsoftJsonSerializer(serializerSettings);
        });
    }

    public HttpRequest CreateRequest(string? endpoint = null, JsonSerializerSettings? serializerSettings = null)
    {
        return new HttpRequest(
            flurlRequest: _flurlClient.Request(endpoint),
            serializerSettings: serializerSettings
        );
    }
}
