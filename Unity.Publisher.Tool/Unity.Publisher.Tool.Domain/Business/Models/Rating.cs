namespace Unity.Publisher.Tool.Domain.Business.Models;

public class Rating
{
    public readonly int Count;

    public readonly double Average;

    public Rating(int count, double average)
    {
        Count = count;
        Average = average;
    }

    public static Rating Zero()
    {
        return new Rating(0, 0);
    }
}
