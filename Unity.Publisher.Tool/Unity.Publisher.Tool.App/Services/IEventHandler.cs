namespace Unity.Publisher.Tool.App.Services;

public interface IEventHandler<TData>
{
    public Task HandleAsync(TData content);
}
