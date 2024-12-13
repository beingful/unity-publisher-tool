using Flurl.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses.Flurl;

public sealed class JsonHttpResponse : HttpResponse, IJsonHttpResponse
{
    private readonly HttpContent _httpContent;

    public static readonly JsonSerializerOptions DefaultSerializerOptions;

    static JsonHttpResponse()
    {
        DefaultSerializerOptions = new()
        {
            IgnoreReadOnlyProperties = false,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
    }

    public JsonHttpResponse(IFlurlResponse response) : base(response)
    {
        _httpContent = response.ResponseMessage.Content;
    }

    public async Task<TResponseModel> ReadAs<TResponseModel>(JsonSerializerOptions? serializerOptions = null)
    {
        return (await _httpContent.ReadFromJsonAsync<TResponseModel>(serializerOptions ?? DefaultSerializerOptions))!;
    }
}
