using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting.Operations;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public class ParagraphFormatting : IFormatting
{
    public ParagraphFormatting() : this(new FormattingOptions())
    {
    }

    public ParagraphFormatting(FormattingOptions options)
    {
        Options = options ?? new FormattingOptions();
        Indent = new Indent(Options);
        Separation = Options.Separator == null
            ? null
            : new Separation(Indent, Options);
        LineFeed = new LineFeed(Indent, Options);
    }

    public FormattingOptions Options { get; }

    public IParagraphFormattingOperation Indent { get; }

    public IFormattingOperation? Separation { get; }

    public IFormattingOperation LineFeed { get; }

    public string Apply(Content content)
    {
        return $"{Apply(content.Text)}\n{Apply(content.InnerDocuments)}";
    }

    private string Apply(string text)
    {
        return Indent.Margin > 0 ? Indent.Apply(text) : text;
    }

    private string Apply(IReadOnlyCollection<IDocument> innerDocuments)
    {
        List<string> paragraphs = new(innerDocuments.Count);

        foreach (IDocument document in innerDocuments)
        {
            paragraphs.Add(document.ToString());
        }

        return Separation?.Apply(paragraphs
                .FindAll(x => string.IsNullOrWhiteSpace(x) == false)
                .ToArray())
            ?? string.Join('\n', paragraphs);
    }
}
