namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public interface IParagraphFormattingOperation : IFormattingOperation
{
    public int Margin { get; set; }
}
