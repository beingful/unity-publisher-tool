namespace Unity.Publisher.Tool.Domain.Data;

public interface IHandler<TData>
{
    public Task HandleAsync(TData data);
}
