using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

public sealed class DownloadsReport
{
    public readonly Download Download;

    public DownloadsReport(Download download)
    {
        Download = download;
    }
}
