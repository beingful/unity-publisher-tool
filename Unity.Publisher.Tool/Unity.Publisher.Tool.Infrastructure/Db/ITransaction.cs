namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface ITransaction
{
    Task ExecuteAsync();
}
