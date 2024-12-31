namespace Unity.Publisher.Tool.Domain.Data;

public interface IProvider<TOut>
{
    TOut Provide();
}

public interface IProvider<TIn, TOut>
{
    TOut Provide(TIn input);
}
