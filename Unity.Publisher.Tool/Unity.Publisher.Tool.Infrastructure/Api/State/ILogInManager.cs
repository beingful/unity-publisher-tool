namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public interface ILogInManager
{
    public Task LogInAsync(CancellationToken cancellationToken = default);
}

public interface ILogInManager<TExternalApi> : ILogInManager
    where TExternalApi : ExternalApi
{
}
