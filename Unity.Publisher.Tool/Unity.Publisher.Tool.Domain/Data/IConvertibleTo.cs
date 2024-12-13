namespace Unity.Publisher.Tool.Domain.Data;

public interface IConvertibleTo<TModel>
{
    TModel Convert();
}
