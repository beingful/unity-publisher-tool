namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public interface IDocument
{
    Title Title { get; }

    public int Depth { get; internal set; }

    IDocument AddInner(IDocument content);

    string ToString();
}
