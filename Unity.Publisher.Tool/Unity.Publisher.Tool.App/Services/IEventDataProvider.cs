namespace Unity.Publisher.Tool.App.Services;

public interface IEventDataProvider<TContent>
{
    Task<TContent> ProvideAsync();
}
