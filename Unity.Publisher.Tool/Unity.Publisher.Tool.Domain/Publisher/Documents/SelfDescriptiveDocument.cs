namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public class SelfDescriptiveDocument : Document
{
    public SelfDescriptiveDocument(Content content, Metadata metadata) : base(content, metadata)
    {
    }

    public override string Summary()
    {
        return Metadata.Description;
    }
}
