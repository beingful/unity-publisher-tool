using FluentAssertions;
using Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Test.Comparers;

public partial class ReviewsComparerTest
{
    [Theory]
    [MemberData(nameof(EqualReviews))]
    public void EqualReviews_Different_ShouldBeFalse(Reviews first, Reviews second)
    {
        ReviewsComparer comparer = new();

        bool different = comparer.Different(first, second);

        different.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(DifferentReviews))]
    public void DifferentReviews_Different_ShouldBeTrue(Reviews first, Reviews second)
    {
        ReviewsComparer comparer = new();

        bool different = comparer.Different(first, second);

        different.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ReviewsWithEmptyDifference))]
    public void Reviews_Difference_ShouldBeEmpty(Reviews first, Reviews second)
    {
        ReviewsComparer comparer = new();

        Reviews difference = comparer.Difference(first, second);

        difference.IsEmpty.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ReviewsWithNotEmptyDifference))]
    public void Reviews_Difference_ShouldBeEqualToResult(Reviews left, Reviews right, Reviews result)
    {
        ReviewsComparer comparer = new();

        Reviews difference = comparer.Difference(left, right);

        difference.IsEmpty.Should().Be(result.IsEmpty);
        difference.Count.Should().Be(result.Count);
        difference.Collection.Should().BeEquivalentTo(result.Collection);
    }
}
