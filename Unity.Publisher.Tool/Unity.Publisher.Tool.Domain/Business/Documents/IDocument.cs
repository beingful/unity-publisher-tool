namespace Unity.Publisher.Tool.Domain.Business.Documents;

public interface IDocument
{
    Title Title { get; }

    public int Depth { get; internal set; }

    IDocument AddInner(IDocument content);

    string ToString();
}
