namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public interface ISessionManager
{
    bool IsSessionAlive();

    Task SetUpSessionAsync(CancellationToken cancellationToken = default);
}

public interface ISessionManager<out TExternalApi> : ISessionManager
    where TExternalApi : ExternalApi
{
}
