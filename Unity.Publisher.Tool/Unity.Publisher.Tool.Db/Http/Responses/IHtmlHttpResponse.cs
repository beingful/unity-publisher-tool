using Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

namespace Unity.Publisher.Tool.Infrastructure.Http.Responses;

public interface IHtmlHttpResponse : IHttpResponse
{
    IHtmlContent Content { get; }
}
