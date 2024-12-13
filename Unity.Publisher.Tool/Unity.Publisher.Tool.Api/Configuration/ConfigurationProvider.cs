namespace Unity.Publisher.Tool.Configuration;

public sealed class ConfigurationProvider
{
    private readonly IConfiguration _configuration;

    public ConfigurationProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TSection GetSection<TSection>(string? section = null) where TSection : IConfigurationSection
    {
        return _configuration.GetSection(section ?? typeof(TSection).Name).Get<TSection>()!;
    }
}
