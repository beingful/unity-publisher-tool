namespace Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

public interface IFormattingOperation
{
    string Apply(params string[] paragraphs);
}
