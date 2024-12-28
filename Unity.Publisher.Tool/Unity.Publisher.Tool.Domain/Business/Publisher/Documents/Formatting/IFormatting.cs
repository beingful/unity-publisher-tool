namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public interface IFormatting
{
    FormattingOptions Options { get; }

    IParagraphFormattingOperation Indent { get; }

    IFormattingOperation Separation { get; }
}
