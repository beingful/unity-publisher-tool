namespace Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

public interface IParagraphFormattingOperation : IFormattingOperation
{
    public int Margin { get; set; }
}
