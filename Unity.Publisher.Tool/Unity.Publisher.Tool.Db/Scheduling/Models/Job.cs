namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

public class Job
{
    public readonly string Id;
    public readonly TriggerTime Time;
    //public readonly KeyValuePair<string, object>[] Parameters;

    public Job(string id, TriggerTime time/*KeyValuePair<string, object>[] parameters*/)
    {
        Id = id;
        Time = time;
        //Parameters = parameters;
    }

    //public bool IsParameterless => Parameters.Length == 0;

    //public static Job Create(string id, TriggerTime time, params KeyValuePair<string, object>[] parameters)
    //{
    //    return new Job(id, time, parameters);
    //}

    //public static Job Parameterless(string id, TriggerTime time)
    //{
    //    return new Job(id, time, []);
    //}
}
