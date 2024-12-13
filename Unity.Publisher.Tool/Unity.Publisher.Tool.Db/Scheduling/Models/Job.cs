namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

public class Job
{
    public readonly string Id;
    public readonly TriggerTime Time;

    public Job(string id, TriggerTime time)
    {
        Id = id;
        Time = time;
    }
}
