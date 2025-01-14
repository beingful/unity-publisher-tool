namespace Unity.Publisher.Tool.Infrastructure.Scheduling;

internal class TimePeriod
{
    public readonly int Day;
    public readonly int Hour;
    public readonly int Minute;

    public TimePeriod(int day, int hour, int minute)
    {
        Day = day;
        Hour = hour;
        Minute = minute;
    }
}
