namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public class DocumentFormatting : IFormatting
{
    private readonly IFormatting _formatting;

    public DocumentFormatting(IFormatting baseFormatting)
    {
        _formatting = baseFormatting;
    }

    public FormattingOptions Options => _formatting.Options;

    public IParagraphFormattingOperation Indent => _formatting.Indent;

    public IFormattingOperation? Separation => _formatting.Separation;

    public IFormattingOperation LineFeed => _formatting.LineFeed;

    public string Apply(Content content)
    {
        return _formatting.Apply(content);
    }
}
