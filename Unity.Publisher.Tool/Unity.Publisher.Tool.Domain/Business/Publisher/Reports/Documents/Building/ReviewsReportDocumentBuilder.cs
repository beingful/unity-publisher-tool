//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Documents.Building;

//public class ReviewsReportDocumentBuilder : IDocumentBuilder<ReviewsReport>
//{
//    private readonly IDocumentBuilder<Review> _contentBuilder;
//    private readonly IFormatter<ReviewsReport> _formatter;

//    public ReviewsReportDocumentBuilder(
//        IDocumentBuilder<Review> contentBuilder,
//        IFormatter<ReviewsReport> formatter)
//    {
//        _contentBuilder = contentBuilder;
//        _formatter = formatter;
//    }

//    public string Build(ReviewsReport report, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        StringBuilder document = new();

//        AppendHeader(document);
//        AppendContent(report.Reviews, document);
//        AppendFooter(report, document);

//        return document.ToString();
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        _formatter.SetMargin(settings.Margin);
//    }

//    private void AppendHeader(StringBuilder document, BuildSettings? settings = null)
//    {
//        if (settings.HasValue)
//        {
//            AdjustFormatting(settings.Value);
//        }

//        document.AppendLine(value: _formatter.FormatLine("REVIEWS:\n"));
//    }

//    private void AppendContent(Review[] reviews, StringBuilder document)
//    {
//        string separator = _formatter.ContentSeparator;

//        _contentBuilder.AdjustFormatting(new BuildSettings
//        {
//            Margin = _formatter.Options.Padding + 1
//        });

//        for (int i = 0; i < reviews.Length; ++i)
//        {
//            document
//                .AppendLine(value: _contentBuilder.Build(reviews[i]))
//                .AppendLine(value: separator);
//        }
//    }

//    private void AppendFooter(ReviewsReport report, StringBuilder document)
//    {
//        document.AppendLine(value: _formatter.FormatLine($"Average rating: {report.AverageRating}"));
//    }
//}
