namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

public class JobParameter
{
    public readonly string Name;
    public readonly object Value;

    public JobParameter(string name, object value)
    {
        Name = name;
        Value = value;
    }
}
