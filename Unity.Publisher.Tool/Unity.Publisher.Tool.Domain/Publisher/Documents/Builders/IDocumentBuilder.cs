using Unity.Publisher.Tool.Domain.Publisher.Documents;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders;

public interface IDocumentBuilder<TContent>
{
    IDocument Build(TContent content);
}
