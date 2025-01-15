using Hangfire;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire;

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
        string crone = CroneRepresentation(triggerTime.Time.Minute) + " "
            + CroneRepresentation(triggerTime.Time.Hour) + " "
            + CroneRepresentation(triggerTime.Time.Day) + " "
            + CroneRepresentation(0) + " "
            + CroneRepresentation(0);

        return crone;
    }

    private static string CroneRepresentation(int time)
    {
        return time > 0 ? $"*/{time}" : "*";
    }
}
