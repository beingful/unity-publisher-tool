namespace Unity.Publisher.Tool.Domain.Data.Providers;

public interface IKeyedProvider<TKey, TComponent>
{
    TComponent Provide(TKey key);
}
