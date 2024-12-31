namespace Unity.Publisher.Tool.Infrastructure.Db;

public interface IStorage<TTransaction, TStorage>
    where TTransaction : ITransaction, TStorage
    where TStorage : IStorage<TTransaction, TStorage>
{
    TTransaction CreateTransaction();

    ITransactionQueue<TTransaction, TStorage> CreateTransactionQueue(TTransaction transaction);
}
