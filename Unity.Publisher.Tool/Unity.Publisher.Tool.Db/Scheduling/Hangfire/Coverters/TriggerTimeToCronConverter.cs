using Hangfire;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Coverters;

public static class TriggerTimeToCronConverter
{
    public static string Convert(TriggerTime triggerTime)
    {
        return triggerTime.Trigger switch
        {
            Trigger.Monthly => ToMontlyCrone(triggerTime),
            Trigger.Interval => ToIntervalCrone(triggerTime),
            _ => throw new ArgumentException($"Unhandled time trigger type: {triggerTime.Trigger}.")
        };
    }

    private static string ToMontlyCrone(TriggerTime triggerTime)
    {
        return Cron.Monthly(triggerTime.Time.Day, triggerTime.Time.Hour, triggerTime.Time.Minute);
    }

    private static string ToIntervalCrone(TriggerTime triggerTime)
    {
        return $"*/{triggerTime.Time.Minute} */{triggerTime.Time.Hour} * * *";
    }
}
