namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public interface IParagraphFormattingOperation : IFormattingOperation
{
    public int Margin { get; set; }
}
