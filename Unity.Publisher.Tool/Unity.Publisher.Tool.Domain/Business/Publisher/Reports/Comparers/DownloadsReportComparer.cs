//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;
//using Unity.Publisher.Tool.Domain.Data;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Comparers;

//public class DownloadsReportComparer : IDataComparer<DownloadsReport>
//{
//    public bool Different(DownloadsReport first, DownloadsReport second)
//    {
//        return first.Download.Downloads != second.Download.Downloads;
//    }

//    public DownloadsReport Difference(DownloadsReport left, DownloadsReport right)
//    {
//        return new DownloadsReport(
//            download: new Download(
//                product: left.Download.Product,
//                downloads: left.Download.Downloads - right.Download.Downloads,
//                downloaders: left.Download.Downloaders - right.Download.Downloaders));
//    }
//}
