using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

public class Indent : IParagraphFormattingOperation
{
    private readonly FormattingOptions _options;

    public Indent(FormattingOptions options)
    {
        _options = options;
    }

    public int Margin { get; set; }

    public string Apply(params string[] paragraphs)
    {
        string[] formatted = new string[paragraphs.Length];

        for (int i = 0; i < formatted.Length; ++i)
        {
            formatted[i] = Apply(paragraphs[i]);
        }

        return string.Join(Environment.NewLine, formatted);
    }

    private string Apply(string text)
    {
        string[] formatted = text.Split(Environment.NewLine);

        string indent = GetIndent();

        for (int i = 0; i < formatted.Length; ++i)
        {
            formatted[i] = $"{indent}{text[i]}";
        }

        return string.Join(Environment.NewLine, formatted);
    }

    private string GetIndent()
    {
        return new Repeatable('\t', Margin + _options.Padding).ToString();
    }
}
