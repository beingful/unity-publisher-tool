namespace Unity.Publisher.Tool.Domain.Data;

public class ConditionalHandler<TData> : IHandler<TData>
{
    private readonly IHandler<TData> _handler;
    private readonly Predicate<TData> _needsHandling;

    public ConditionalHandler(IHandler<TData> handler, Predicate<TData> handleIf)
    {
        _handler = handler;
        _needsHandling = handleIf;
    }

    public async Task HandleAsync(TData content)
    {
        if (_needsHandling.Invoke(content))
        {
            await _handler.HandleAsync(content);
        }
    }
}
