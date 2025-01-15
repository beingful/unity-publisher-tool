using System.Text.RegularExpressions;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting.Operations;

public class Indent : IParagraphFormattingOperation
{
    private readonly FormattingOptions _options;

    private readonly static Regex _indentPosition;

    private const int _oneIndentCapacity = 4;

    static Indent()
    {
        _indentPosition = new Regex("\\n\\S");
    }

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

        return string.Join('\n', formatted);
    }

    private string Apply(string text)
    {
        string indent = GetIndent();

        string textwithIndents = ApplyIndent(text.TrimStart());

        if (textwithIndents.StartsWith(indent) == false)
        {
            textwithIndents = $"{indent}{textwithIndents}";
        }

        return textwithIndents;
    }

    private string ApplyIndent(string text)
    {
        string indent = GetIndent();

        return _indentPosition.Replace(text, new MatchEvaluator(match =>
        {
            return match.Value.Replace("\n", $"\n{indent}");
        }));
    }

    private string GetIndent()
    {
        return new Repeatable(' ', (Margin + _options.Padding) * _oneIndentCapacity).ToString();
    }
}
