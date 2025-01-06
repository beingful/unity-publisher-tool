namespace Unity.Publisher.Tool.Domain.General;

public interface IKeyedProvider<TKey, TValue>
{
    TValue Provide(TKey input);
}
