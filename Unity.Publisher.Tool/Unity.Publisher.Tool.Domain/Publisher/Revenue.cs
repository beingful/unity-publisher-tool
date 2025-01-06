namespace Unity.Publisher.Tool.Domain.Publisher;

public class Revenue
{
    public readonly decimal ForPeriod;

    public readonly decimal Total;

    public Revenue(decimal forPeriod, decimal total)
    {
        ForPeriod = forPeriod;
        Total = total;
    }

    public static Revenue Zero()
    {
        return new Revenue(0, 0);
    }
}
