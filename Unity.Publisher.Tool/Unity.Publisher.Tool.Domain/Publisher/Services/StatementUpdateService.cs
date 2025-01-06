using Unity.Publisher.Tool.Domain.General;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public class StatementUpdateService : IDataSource<PublisherStatement>
{
    private readonly PublisherStatementService _publisherStatementService;
    private readonly IDataComparer<PublisherStatement> _publisherStatementComparer;
    private readonly IDataStorage _dataStorage;

    public StatementUpdateService(
        PublisherStatementService publisherStatementService,
        IDataComparer<PublisherStatement> publisherStatementComparer,
        IDataStorage dataStorage)
    {
        _publisherStatementService = publisherStatementService;
        _publisherStatementComparer = publisherStatementComparer;
        _dataStorage = dataStorage;
    }

    public async Task<PublisherStatement> GetAsync()
    {
        PublisherStatement storedStatement = _publisherStatementService
            .GetStored();

        PublisherStatement refreshedStatement = await _publisherStatementService
            .RefreshAsync();

        PublisherStatement update;

        if (storedStatement.IsEmpty)
        {
            _dataStorage.Set(refreshedStatement);

            update = PublisherStatement.Empty();
        }

        refreshedStatement.AssetsStatements[0].Sales = new Sales([ new Sale(
            productTag: new ProductTag(product: "2D Laser system", price: 10),
            copiesSold: 1,
            revenue: 10)]);
        //refreshedStatement.AssetsStatements[0].Sales[0].Revenue += 10;
        refreshedStatement.AssetsStatements[0].Downloads.Downloads += 5;
        refreshedStatement.AssetsStatements[0].Downloads.Downloaders += 1;

        if (_publisherStatementComparer.Different(refreshedStatement, storedStatement))
        {
            //_publisherEventDataStorage.Set(refreshedStatement);

            update = _publisherStatementComparer.Difference(refreshedStatement, storedStatement);
        }
        else
        {
            update = PublisherStatement.Empty();
        }

        return update;
    }
}
