namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public struct FormattingOptions
{
    public readonly int Padding;
    public readonly int Margin;
    public readonly Repeatable Indent;
    public readonly Repeatable? Separator;

    private const int _pageSize = 100;

    public FormattingOptions(int padding = 0, int margin = 0,
        char? separatingCharacter = null)
    {
        Padding = padding;
        Margin = margin;
        Indent = new('\t', padding + margin);
        Separator = separatingCharacter.HasValue
            ? new(
                symbol: separatingCharacter.Value,
                length: _pageSize - (padding + margin))
            : null;
    }
}
