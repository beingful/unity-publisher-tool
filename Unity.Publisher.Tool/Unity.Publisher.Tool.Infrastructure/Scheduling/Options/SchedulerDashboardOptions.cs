namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Options;

public class SchedulerDashboardOptions
{
    public static string JsonKey => $"{SchedulerOptions.JsonKey}:Dashboard";

    public required string Path { get; init; }

    public required string Title { get; init; }
}
