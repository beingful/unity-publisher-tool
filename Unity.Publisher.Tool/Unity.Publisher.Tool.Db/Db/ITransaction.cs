namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface ITransaction
{
    ITransaction Enqueue(Func<IStorage, Task> dbRequest);

    Task ExecuteAsync();
}
