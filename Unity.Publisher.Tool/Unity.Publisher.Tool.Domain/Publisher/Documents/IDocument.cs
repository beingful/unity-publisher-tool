namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public interface IDocument
{
    Metadata Metadata { get; }

    public int Depth { get; internal set; }

    IDocument AddInner(IDocument content);

    string Summary();

    string Text();
}
