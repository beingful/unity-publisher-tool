using Unity.Publisher.Tool.Domain.General;

namespace Unity.Publisher.Tool.Domain.Publisher.Comparers;

public class DownloadComparer : IDataComparer<Download>
{
    public bool Different(Download first, Download second)
    {
        return first.Downloads != second.Downloads;
    }

    public Download Difference(Download left, Download right)
    {
        return left.IsEmpty ? left : CalculateDifference(left, right);
    }

    private Download CalculateDifference(Download left, Download right)
    {
        return new Download(
            product: left.Product,
            downloads: Math.Max(left.Downloads - right.Downloads, 0),
            downloaders: Math.Max(left.Downloaders - right.Downloaders, 0));
    }
}
