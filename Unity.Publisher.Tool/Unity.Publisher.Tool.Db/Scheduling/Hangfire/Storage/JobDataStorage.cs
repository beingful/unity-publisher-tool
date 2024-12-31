using Hangfire.Storage;
using System.Text.Json;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Storage;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Storage;

public class JobDataStorage : IJobDataStorage
{
    private readonly IStorageConnection _storageConnection;

    public JobDataStorage(IStorageConnection storageConnection)
    {
        _storageConnection = storageConnection;
    }

    public void Insert(string jobId, Dictionary<string, object> data)
    {
        foreach (KeyValuePair<string, object> entry in data)
        {
            _storageConnection.SetJobParameter(
                id: jobId,
                name: entry.Key,
                value: JsonSerializer.Serialize(entry.Value));
        }
    }

    public TData Fetch<TData>(string jobId, string parameterName)
    {
        string parameter = _storageConnection.GetJobParameter(jobId, parameterName);

        return JsonSerializer.Deserialize<TData>(parameter)!;
    }
}
