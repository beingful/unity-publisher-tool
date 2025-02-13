namespace Unity.Publisher.Tool.Domain.General;

public interface IConvertible<TModel>
{
    TModel Convert();
}
