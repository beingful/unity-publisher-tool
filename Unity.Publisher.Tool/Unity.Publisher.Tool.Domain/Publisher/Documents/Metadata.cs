namespace Unity.Publisher.Tool.Domain.Publisher.Documents;

public class Metadata
{
    public readonly string Title;
    public readonly string Description;

    private Metadata(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static Metadata Create(string title, string description)
    {
        return new Metadata(title, description);
    }

    public static Metadata WithTitle(string title)
    {
        return new Metadata(title, string.Empty);
    }

    public static Metadata WithDescription(string description)
    {
        return new Metadata(string.Empty, description);
    }

    public static Metadata Empty()
    {
        return new Metadata(string.Empty, string.Empty);
    }
}
