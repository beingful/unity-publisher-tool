namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public class Document : IDocument
{
    private readonly Content _content;
    private readonly Metadata _metadata;

    protected Document(Content content, Metadata metadata)
    {
        _content = content;
        _metadata = metadata;
    }

    public Metadata Metadata => _metadata;

    public int Depth
    {
        get => _content.Depth;
        set => _content.Depth = value;
    }

    public static Document Create(Content content, Metadata metadata)
    {
        Document document = new(content, metadata);

        return document;
    }

    public static Document Create(Content content)
    {
        Document document = new(content, Metadata.Empty());

        return document;
    }

    public IDocument AddInner(IDocument document)
    {
        _content.AddInner(document);

        return this;
    }

    public virtual string Summary()
    {
        string summary = _metadata.Description;

        List<string> innerSummaries = [];

        foreach (IDocument document in _content.InnerDocuments)
        {
            string innerDocumentSummary = document.Summary();

            if (string.IsNullOrWhiteSpace(innerDocumentSummary) == false)
            {
                innerSummaries.Add(innerDocumentSummary);
            }
        }

        if (innerSummaries.Count > 0)
        {
            string innerSummary = string.Join(", ", innerSummaries);

            summary += string.IsNullOrWhiteSpace(summary)
                ? innerSummary
                : $": {innerSummary}";
        }

        return summary;
    }

    public string Text()
    {
        return _content.Formatted();
    }
}
