using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents;

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
        return $"{Text()}\n{InnerContent()}\n";
    }

    private string Text()
    {
        return _formatting.Indent.Apply(_text);
    }

    private string InnerContent()
    {
        List<string> paragraphs = new(_innerDocuments.Count);

        foreach (IDocument document in _innerDocuments)
        {
            paragraphs.Add(document.ToString());
        }

        return _formatting.Separation.Apply(
            paragraphs
                .FindAll(x => string.IsNullOrWhiteSpace(x) == false)
                .ToArray());
    }
}
