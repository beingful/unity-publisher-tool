using Unity.Publisher.Tool.Domain.Business.Models;
using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Domain.Business.Comparers;

public class ReviewsComparer : IDataComparer<Reviews>
{
    public bool Different(Reviews first, Reviews second)
    {
        return first.Collection.Select(x => x.Id)
            .SequenceEqual(second.Collection.Select(x => x.Id)) == false;
    }

    public Reviews Difference(Reviews left, Reviews right)
    {
        HashSet<long> newReviews = left.Collection.Select(x => x.Id)
            .Except(right.Collection.Select(x => x.Id))
            .ToHashSet();

        return new Reviews([.. left.Collection.Where(x => newReviews.Contains(x.Id))]);
    }
}
