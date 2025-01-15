namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting.Operations;

public class Separation : IFormattingOperation
{
    private readonly IParagraphFormattingOperation _indent;
    private readonly FormattingOptions _options;

    public Separation(IParagraphFormattingOperation indent, FormattingOptions options)
    {
        _indent = indent;
        _options = options;
    }

    public string Apply(params string[] paragraphs)
    {
        return string.Join($"\n{ParagraphSeparator()}\n", paragraphs);
    }

    private string ParagraphSeparator()
    {
        string separator = string.Empty;

        if (_options.Separator.HasValue)
        {
            separator = new Repeatable(
                symbol: _options.Separator.Value,
                length: _options.LineCapacity - _options.Padding).ToString();
        }

        return _indent.Apply(separator);
    }
}
