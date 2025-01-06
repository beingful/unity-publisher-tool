using Hangfire.Storage;
using System.Text.Json;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class ScheduleDataStorage : IKeyedDataStorage
{
    private readonly IStorageConnection _storageConnection;
    private readonly JsonSerializerOptions? _serializerOptions;

    private readonly static JsonSerializerOptions? _defaultSerializerOptions;

    static ScheduleDataStorage()
    {
        _defaultSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = true
        };
    }

    public ScheduleDataStorage(IStorageConnection storageConnection, JsonSerializerOptions? serializerOptions = null)
    {
        _storageConnection = storageConnection;
        _serializerOptions = serializerOptions ?? _defaultSerializerOptions;
    }

    public void Insert(string jobId, Dictionary<string, object> data)
    {
        foreach (KeyValuePair<string, object> entry in data)
        {
            _storageConnection.SetJobParameter(
                id: jobId,
                name: entry.Key,
                value: JsonSerializer.Serialize(entry.Value, _serializerOptions));
        }
    }

    public TData? Fetch<TData>(string jobId, string parameterName) where TData : class
    {
        string? parameter = _storageConnection.GetJobParameter(jobId, parameterName);

        return string.IsNullOrWhiteSpace(parameter)
            ? null
            : JsonSerializer.Deserialize<TData>(parameter, _serializerOptions)!;
    }
}
