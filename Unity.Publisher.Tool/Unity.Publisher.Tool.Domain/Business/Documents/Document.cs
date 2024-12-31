namespace Unity.Publisher.Tool.Domain.Business.Documents;

public class Document : IDocument
{
    private readonly Title _title;
    private readonly Content _content;

    private Document(Title title, Content content)
    {
        _title = title;
        _content = content;
    }

    public Title Title => _title;

    public int Depth
    {
        get => _content.Depth;
        set => _content.Depth = value;
    }

    public static Document Create(Title title, Content content)
    {
        Document document = new(title, content);

        document.Depth = 0;

        return document;
    }

    public static Document CreateParagraph(Content content)
    {
        return new Document(Title.Empty(), content);
    }

    public IDocument AddInner(IDocument document)
    {
        _content.AddInner(document);

        return this;
    }

    public override string ToString()
    {
        return _content.ToString();
    }
}
