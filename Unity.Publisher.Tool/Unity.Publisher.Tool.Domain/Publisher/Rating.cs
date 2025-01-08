namespace Unity.Publisher.Tool.Domain.Publisher;

public class Rating
{
    public readonly int Count;

    public readonly double Average;

    private readonly double _maximum;

    public Rating(int count, double average, double maximum = 5)
    {
        Count = count;
        Average = average;
        _maximum = maximum;
    }

    public double Maximum => _maximum;

    public static Rating Zero()
    {
        return new Rating(0, 0);
    }
}
