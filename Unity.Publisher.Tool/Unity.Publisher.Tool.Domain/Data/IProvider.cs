namespace Unity.Publisher.Tool.Domain.Data;

public interface IProvider<TContent>
{
    TContent Provide();
}
