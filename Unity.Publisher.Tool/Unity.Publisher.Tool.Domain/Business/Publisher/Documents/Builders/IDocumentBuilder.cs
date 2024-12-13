namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

public interface IDocumentBuilder
{
    string Build<TContent>(TContent content);
}

public interface IDocumentBuilder<TContent>
{
    string Build(TContent content, BuildSettings? settings = null);

    void AdjustFormatting(BuildSettings settings);
}
