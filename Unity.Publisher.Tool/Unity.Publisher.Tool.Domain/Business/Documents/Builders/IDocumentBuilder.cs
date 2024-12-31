using Unity.Publisher.Tool.Domain.Business.Documents;

namespace Unity.Publisher.Tool.Domain.Business.Documents.Builders;

public interface IDocumentBuilder<TContent>
{
    IDocument Build(TContent content);
}
