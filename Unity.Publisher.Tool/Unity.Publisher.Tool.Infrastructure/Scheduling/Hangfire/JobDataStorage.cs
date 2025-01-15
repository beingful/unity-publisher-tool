using Hangfire.Storage;
using Hangfire;
using System.Text.Json;
using Unity.Publisher.Tool.Domain.Storage;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

public class JobDataStorage : IKeyedDataStorage
{
    private readonly JsonSerializerOptions? _serializerOptions;

    private readonly static JsonSerializerOptions? _defaultSerializerOptions;

    static JobDataStorage()
    {
        _defaultSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = true
        };
    }

    public JobDataStorage(JsonSerializerOptions? serializerOptions = null)
    {
        _serializerOptions = serializerOptions ?? _defaultSerializerOptions;
    }

    public void Insert(string jobId, KeyValuePair<string, object> parameter)
    {
        using IStorageConnection storage = JobStorage.Current.GetConnection();

        storage.SetJobParameter(
            id: jobId,
            name: parameter.Key,
                value: JsonSerializer.Serialize(parameter.Value, _serializerOptions));
    }

    public TData? Fetch<TData>(string jobId, string parameterName) where TData : class
    {
        using IStorageConnection storage = JobStorage.Current.GetConnection();

        string? parameter = storage.GetJobParameter(jobId, parameterName);

        return string.IsNullOrWhiteSpace(parameter)
            ? null
            : JsonSerializer.Deserialize<TData>(parameter, _serializerOptions)!;
    }
}
