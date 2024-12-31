namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

public interface IJobDataStorage
{
    void Insert(string jobId, Dictionary<string, object> data);

    TData Fetch<TData>(string jobId, string parameterName);
}
