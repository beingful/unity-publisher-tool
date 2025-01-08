namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public class Job
{
    public readonly string Id;
    public readonly TriggerTime TriggerTime;

    public Job(string id, TriggerTime triggerTime)
    {
        Id = id;
        TriggerTime = triggerTime;
    }
}
