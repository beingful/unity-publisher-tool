//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;
//using Unity.Publisher.Tool.Domain.Data;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Comparers;

//public class AssetReportComparer : IDataComparer<AssetReport>
//{
//    private readonly IDataComparer<SalesReport> _salesReportComparer;
//    private readonly IDataComparer<ReviewsReport> _reviewsReportComparer;
//    private readonly IDataComparer<DownloadsReport> _downloadsReportComparer;

//    public AssetReportComparer(
//        IDataComparer<SalesReport> salesReportComparer,
//        IDataComparer<ReviewsReport> reviewsReportComparer,
//        IDataComparer<DownloadsReport> downloadsReportComparer)
//    {
//        _salesReportComparer = salesReportComparer;
//        _reviewsReportComparer = reviewsReportComparer;
//        _downloadsReportComparer = downloadsReportComparer;
//    }

//    public bool Different(AssetReport first, AssetReport second)
//    {
//        return first.Asset.Id == second.Asset.Id
//            && (_salesReportComparer.Different(first.SalesReport, second.SalesReport)
//                || _reviewsReportComparer.Different(first.ReviewsReport, second.ReviewsReport)
//                || _downloadsReportComparer.Different(first.DownloadsReport, second.DownloadsReport));
//    }

//    public AssetReport Difference(AssetReport left, AssetReport right)
//    {
//        return new AssetReport(
//            asset: left.Asset,
//            salesReport: _salesReportComparer.Difference(left.SalesReport, right.SalesReport),
//            reviewsReport: _reviewsReportComparer.Difference(left.ReviewsReport, right.ReviewsReport),
//            downloadsReport: _downloadsReportComparer.Difference(left.DownloadsReport, right.DownloadsReport));
//    }
//}
