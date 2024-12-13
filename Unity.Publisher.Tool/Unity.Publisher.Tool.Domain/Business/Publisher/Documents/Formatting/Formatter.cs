using System.Text;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public class Formatter : IFormatter
{
    private readonly static FormattingOptions _defaultOptions;

    static Formatter()
    {
        _defaultOptions = new FormattingOptions();
    }

    public Formatter(FormattingOptions? options = null)
    {
        Options = options ?? _defaultOptions;
    }

    public FormattingOptions Options { get; private set; }

    public string ContentSeparator => FormatLine(
        line: Options.Separator?.String ?? string.Empty);

    public void SetMargin(int margin)
    {
        Options = new FormattingOptions(
            padding: Options.Padding,
            margin: margin,
            separatingCharacter: Options.Separator?.Symbol);
    }

    public string FormatLines(string[] lines)
    {
        StringBuilder text = new();

        foreach (var line in lines)
        {
            text.AppendLine(FormatLine(line));
        }

        return text.ToString();
    }

    public string FormatLine(string line)
    {
        return $"{Options.Indent.String}{line}";
    }
}
