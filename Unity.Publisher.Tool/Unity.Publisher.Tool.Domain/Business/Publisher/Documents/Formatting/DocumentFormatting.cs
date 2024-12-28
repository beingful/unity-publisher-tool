namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public class DocumentFormatting : IFormatting
{
    public DocumentFormatting() : this(new FormattingOptions())
    {
    }

    public DocumentFormatting(FormattingOptions options)
    {
        Options = options ?? new FormattingOptions();
        Indent = new Indent(Options);
        Separation = new Separation(Indent, Options);
    }

    public FormattingOptions Options { get; }

    public IParagraphFormattingOperation Indent { get; }

    public IFormattingOperation Separation { get; }
}
