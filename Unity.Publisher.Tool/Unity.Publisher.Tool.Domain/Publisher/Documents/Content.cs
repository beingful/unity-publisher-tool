using Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public class Content
{
    private readonly string _text;
    private readonly List<IDocument> _innerDocuments;
    private readonly IFormatting _formatting;

    public Content(string text, IFormatting formatting)
    {
        _text = text;
        _formatting = formatting;
        _innerDocuments = [];
    }

    public string Text => _text;

    public IReadOnlyCollection<IDocument> InnerDocuments => _innerDocuments;

    public int Depth
    {
        get => _formatting.Indent.Margin;
        set
        {
            _formatting.Indent.Margin = value;
            _innerDocuments.ForEach(x => x.Depth = value + 1);
        }
    }

    public void AddInner(IDocument document)
    {
        document.Depth = Depth + 1;

        _innerDocuments.Add(document);
    }

    public override string ToString()
    {
        return _formatting.Apply(this);
    }
}
