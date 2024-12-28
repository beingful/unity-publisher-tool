namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public interface IFormattingOperation
{
    string Apply(params string[] paragraphs);
}
