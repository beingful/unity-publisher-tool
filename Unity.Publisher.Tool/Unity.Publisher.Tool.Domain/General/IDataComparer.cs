namespace Unity.Publisher.Tool.Domain.General;

public interface IDataComparer<TModel>
{
    bool Different(TModel first, TModel second);

    TModel Difference(TModel left, TModel right);
}
