namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

public class TriggerTime
{
    internal readonly TimePeriod Time;
    internal readonly Trigger Trigger;

    private TriggerTime(TimePeriod time, Trigger trigger)
    {
        Time = time;
        Trigger = trigger;
    }

    public static TriggerTime Monthly(int minute = 0, int hour = 0, int day = 1)
    {
        return new TriggerTime(new TimePeriod(day, hour, minute), Trigger.Monthly);
    }

    public static TriggerTime FromTimeInterval(int minutes, int hours = 0, int days = 0)
    {
        return new TriggerTime(new TimePeriod(days, hours, minutes), Trigger.Interval);
    }
}
