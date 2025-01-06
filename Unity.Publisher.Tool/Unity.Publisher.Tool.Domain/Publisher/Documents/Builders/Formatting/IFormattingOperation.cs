namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public interface IFormattingOperation
{
    string Apply(params string[] paragraphs);
}
