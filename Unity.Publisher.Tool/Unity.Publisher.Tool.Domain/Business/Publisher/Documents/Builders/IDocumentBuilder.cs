namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public interface IDocumentBuilder<TContent>
{
    IDocument Build(TContent content);
}
