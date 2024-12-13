namespace Unity.Publisher.Tool.Infrastructure.Http.Responses;

public interface IHttpResponse : IDisposable
{
    string? RequestUrl();
}
