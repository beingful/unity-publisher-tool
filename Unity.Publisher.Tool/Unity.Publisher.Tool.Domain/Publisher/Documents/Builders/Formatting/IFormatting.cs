namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public interface IFormatting
{
    FormattingOptions Options { get; }

    IParagraphFormattingOperation Indent { get; }

    IFormattingOperation? Separation { get; }

    IFormattingOperation LineFeed { get; }

    string Apply(Content content);
}
