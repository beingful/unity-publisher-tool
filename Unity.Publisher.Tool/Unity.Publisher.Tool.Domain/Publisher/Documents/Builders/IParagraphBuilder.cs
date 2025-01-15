namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public interface IParagraphBuilder<TContent>
{
    IDocument Build(TContent content);
}
