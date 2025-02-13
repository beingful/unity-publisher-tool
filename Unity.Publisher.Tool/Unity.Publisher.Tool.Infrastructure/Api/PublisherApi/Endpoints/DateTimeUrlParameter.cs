namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Endpoints;

internal class DateTimeUrlParameter
{
    private readonly DateTime _value;

    public DateTimeUrlParameter(DateTime value)
    {
        _value = value;
    }

    public override string ToString()
    {
        return $"{_value.Year * 100 + _value.Month}";
    }
}
