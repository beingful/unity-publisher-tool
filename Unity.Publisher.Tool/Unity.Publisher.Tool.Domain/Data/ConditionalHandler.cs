namespace Unity.Publisher.Tool.Domain.Data;

public class ConditionalHandler<TData> : IHandler<TData>
{
    private readonly IHandler<TData> _handler;
    private readonly Predicate<TData> _canHandle;

    public ConditionalHandler(IHandler<TData> handler, Predicate<TData> canHandle)
    {
        _handler = handler;
        _canHandle = canHandle;
    }

    public async Task HandleAsync(TData content)
    {
        if (_canHandle(content))
        {
            await _handler.HandleAsync(content);
        }
    }
}
