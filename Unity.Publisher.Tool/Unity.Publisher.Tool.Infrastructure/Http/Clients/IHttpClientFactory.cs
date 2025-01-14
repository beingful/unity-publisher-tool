namespace Unity.Publisher.Tool.Infrastructure.Http.Clients;

public interface IHttpClientFactory
{
    IHttpClient CreateFor<TService>();
}
