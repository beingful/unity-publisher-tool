namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

public class SchedulingOptions
{
    public static string Path => "Scheduling";

    public required int JobsLimit {  get; init; }
}
