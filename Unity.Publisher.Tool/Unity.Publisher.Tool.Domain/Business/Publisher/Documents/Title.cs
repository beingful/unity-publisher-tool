namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents;

public class Title
{
    public readonly string Name;
    public readonly string Description;

    public Title(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Title Empty()
    {
        return new Title(name: string.Empty, description: string.Empty);
    }
}
