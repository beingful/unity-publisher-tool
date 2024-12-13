using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Db.Entities;

namespace Unity.Publisher.Tool.App.Services.Statement;

public class PublisherStatementHandler
{
    private readonly IStorage _storage;
    private readonly Month _month;

    public PublisherStatementHandler(IStorage storage, Month month)
    {
        _storage = storage;
        _month = month;
    }

    public async Task HandleNoStoredStatementAsync(PublisherStatement latestStatement)
    {
        await _storage.CreateTransaction()
            .Enqueue(repository =>
            {
                return repository.InsertAsync(new PublisherStatementEntity()
                {
                    Id = _month.Name.ToUpper(),
                    PublisherStatement = latestStatement
                });
            })
            .Enqueue(repository =>
            {
                return repository.RemoveAsync<PublisherStatementEntity>(
                    id: _month.Previous().Name);
            })
            .ExecuteAsync()
            .ConfigureAwait(false);
    }

    public async Task HandleStatementUpdatedAsync(PublisherStatement latestStatement)
    {
        await _storage.UpdateAsync(new PublisherStatementEntity()
        {
            Id = _month.Name.ToUpper(),
            PublisherStatement = latestStatement
        }).ConfigureAwait(false);
    }
}
