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
        return new Download(
            product: left.Product,
            downloads: left.Downloads - right.Downloads,
            downloaders: left.Downloaders - right.Downloaders);
    }
}
