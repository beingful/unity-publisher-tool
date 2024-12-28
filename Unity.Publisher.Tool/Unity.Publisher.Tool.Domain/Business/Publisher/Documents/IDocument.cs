namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents;

public interface IDocument
{
    Title Title { get; }

    public int Depth { get; internal set; }

    IDocument AddInner(IDocument content);

    string ToString();
}
