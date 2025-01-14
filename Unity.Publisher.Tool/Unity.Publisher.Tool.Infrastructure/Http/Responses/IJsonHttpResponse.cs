using System.Text.Json;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses;

public interface IJsonHttpResponse : IHttpResponse
{
    Task<TResponseModel> ReadAs<TResponseModel>(JsonSerializerOptions? serializerOptions = null);
}
