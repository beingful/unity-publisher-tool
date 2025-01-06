namespace Unity.Publisher.Tool.Domain.Storage;

public interface IDataNameProvider
{
    string Provide<TData>();
}
