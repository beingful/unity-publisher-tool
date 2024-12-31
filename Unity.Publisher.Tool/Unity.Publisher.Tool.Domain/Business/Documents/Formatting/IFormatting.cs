namespace Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

public interface IFormatting
{
    FormattingOptions Options { get; }

    IParagraphFormattingOperation Indent { get; }

    IFormattingOperation Separation { get; }
}
