namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

public class SchedulerOptions
{
    public static string JsonKey => "Scheduler";

    public required int Capacity {  get; init; }
}
