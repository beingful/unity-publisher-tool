using Unity.Publisher.Tool.Domain.Business.Publisher.Events.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;

public class PublisherStatementComparer : IDataComparer<PublisherStatement>
{
    private readonly IDataComparer<AssetStatement> _assetEventComparer;

    public PublisherStatementComparer(IDataComparer<AssetStatement> assetEventComparer)
    {
        _assetEventComparer = assetEventComparer;
    }

    public bool Different(PublisherStatement first, PublisherStatement second)
    {
        return AnyAssetAdded(first.AssetsStatements, second.AssetsStatements)
            || AnyAssetChanged(first.AssetsStatements, second.AssetsStatements);
    }

    public PublisherStatement Difference(PublisherStatement left, PublisherStatement right)
    {
        return new PublisherStatement(
            assetsStatements: Differences(left.AssetsStatements, right.AssetsStatements));
    }

    private AssetStatement[] Differences(AssetStatement[] left, AssetStatement[] right)
    {
        List<AssetStatement> differences = [];

        for (int i = 0, j = 0; i < left.Length;)
        {
            AssetStatement newStatement = left[i];
            AssetStatement? oldStatement = right.ElementAtOrDefault(j);

            if (newStatement.Asset.Id == oldStatement?.Asset.Id)
            {
                if (_assetEventComparer.Different(newStatement, oldStatement))
                {
                    differences.Add(_assetEventComparer.Difference(newStatement, oldStatement));
                }

                ++i;
            }
            else if (oldStatement == null)
            {
                differences.Add(newStatement);

                ++i;
            }
            else
            {
                ++j;
            }
        }

        return [.. differences];
    }

    private bool AnyAssetAdded(AssetStatement[] first, AssetStatement[] seccond)
    {
        return first.Select(x => x.Asset.Id)
            .Except(seccond.Select(x => x.Asset.Id))
            .Any();
    }

    private bool AnyAssetChanged(AssetStatement[] first, AssetStatement[] second)
    {
        bool anyAssetChanged = false;

        for (int i = 0, j = 0; i < second.Length; ++i)
        {
            if (first[j].Asset.Id == second[i].Asset.Id)
            {
                if (_assetEventComparer.Different(first[j], second[i]))
                {
                    anyAssetChanged = true;

                    break;
                }

                ++j;
            }
        }

        return anyAssetChanged;
    }
}
