namespace Unity.Publisher.Tool.Domain.Data;

public interface IKeyedProvider<TKey, TValue>
{
    TValue Provide(TKey input);
}
