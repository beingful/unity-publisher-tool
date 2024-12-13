namespace Unity.Publisher.Tool.Infrastructure.Api.State;

public interface ILogInManager
{
    public Task LogInAsync();
}

public interface ILogInManager<TExternalApi> : ILogInManager
    where TExternalApi : ExternalApi
{
}
