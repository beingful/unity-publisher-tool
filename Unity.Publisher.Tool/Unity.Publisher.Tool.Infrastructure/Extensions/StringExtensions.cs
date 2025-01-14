using System.Text.RegularExpressions;

namespace Unity.Publisher.Tool.Infrastructure.Extensions;

internal static class StringExtensions
{
    public static decimal ToPrice(this string target)
    {
        Regex priceRegex = new("\\d+.\\d{2}");

        return decimal.Parse(priceRegex.Match(target).Value);
    }
}
