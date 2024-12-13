using FluentAssertions;
using Unity.Publisher.Tool.Domain.Business.Publisher.Comparers;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Test.Comparers;

public partial class SaleComparerTests
{
    [Theory]
    [MemberData(nameof(EqualSales))]
    public void EqualSales_Different_ShouldBeFalse(Sale first, Sale second)
    {
        SaleComparer comparer = new();

        bool different = comparer.Different(first, second);

        different.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(DifferentSales))]
    public void DifferentSales_Different_ShouldBeFalse(Sale first, Sale second)
    {
        SaleComparer comparer = new();

        bool different = comparer.Different(first, second);

        different.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(DifferentSalesWithEmptyDifference))]
    public void Sales_Difference_ShouldBeEmpty(Sale left, Sale right)
    {
        SaleComparer comparer = new();

        Sale difference = comparer.Difference(left, right);

        difference.IsEmpty.Should().BeTrue();
        difference.CopiesSold.Should().Be(0);
        difference.Revenue.Should().Be(0);
    }

    [Theory]
    [MemberData(nameof(DifferentSalesBiggerGoFirst))]
    public void Sales_LeftSaleBigger_Difference_ShouldNotBeEmpty(Sale left, Sale right)
    {
        SaleComparer comparer = new();

        Sale difference = comparer.Difference(left, right);

        difference.IsEmpty.Should().BeFalse();

        difference.CopiesSold.Should().BePositive();
        difference.CopiesSold.Should().Be(left.CopiesSold - right.CopiesSold);

        difference.Revenue.Should().BePositive();
        difference.Revenue.Should().Be(left.Revenue - right.Revenue);
    }
}