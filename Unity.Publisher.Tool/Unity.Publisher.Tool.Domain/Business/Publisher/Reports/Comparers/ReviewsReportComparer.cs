//using Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;
//using Unity.Publisher.Tool.Domain.Data;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Comparers;

//public class ReviewsReportComparer : IDataComparer<ReviewsReport>
//{
//    public bool Different(ReviewsReport first, ReviewsReport second)
//    {
//        bool different = first.Reviews.Length != second.Reviews.Length;

//        if (!different)
//        {
//            IEnumerable<long> firstReviews = first.Reviews.Select(x => x.Id);
//            IEnumerable<long> secondReviews = second.Reviews.Select(x => x.Id);

//            different = firstReviews.SequenceEqual(secondReviews) == false;
//        }

//        return different;
//    }

//    public ReviewsReport Difference(ReviewsReport left, ReviewsReport right)
//    {
//        IEnumerable<long> currentReviews = left.Reviews.Select(x => x.Id);
//        IEnumerable<long> previousReviews = right.Reviews.Select(x => x.Id);

//        HashSet<long> newReviews = currentReviews
//            .Except(previousReviews)
//            .ToHashSet();

//        return new ReviewsReport(
//            reviews: left.Reviews
//                .Where(x => newReviews.Contains(x.Id))
//                .ToArray());
//    }
//}
