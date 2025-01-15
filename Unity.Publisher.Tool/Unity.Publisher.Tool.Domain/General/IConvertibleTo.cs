namespace Unity.Publisher.Tool.Domain.General;

public interface IConvertibleTo<TModel>
{
    TModel Convert();
}
