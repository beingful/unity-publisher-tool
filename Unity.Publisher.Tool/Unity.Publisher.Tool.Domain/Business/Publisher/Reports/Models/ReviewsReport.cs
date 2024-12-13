using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Reports.Models;

public sealed class ReviewsReport
{
    public readonly Reviews Reviews;

    public ReviewsReport(Reviews reviews)
    {
        Reviews = reviews;
    }

    public float AverageRating => IsBlank
        ? 0
        : Reviews.Collection.Sum(x => x.Rating) / Reviews.Count;

    public bool IsBlank => Reviews.Count == 0;
}
