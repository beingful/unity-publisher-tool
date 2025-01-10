namespace Unity.Publisher.Tool.Domain.Publisher;

public class Rating
{
    public readonly double Value;

    private readonly double _outOf;

    public Rating(double value, double outOf = 5)
    {
        Value = value;
        _outOf = outOf;
    }

    public double OutOf => _outOf;

    public static Rating Zero()
    {
        return new Rating(0);
    }

    public override string ToString()
    {
        return $"{Value}/{_outOf}";
    }
}
