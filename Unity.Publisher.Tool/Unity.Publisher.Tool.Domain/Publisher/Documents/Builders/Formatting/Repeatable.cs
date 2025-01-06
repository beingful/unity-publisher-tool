namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public struct Repeatable
{
    public readonly char Symbol;
    public readonly int Length;

    public Repeatable(char symbol, int length)
    {
        Symbol = symbol;
        Length = length;
    }

    public override string ToString()
    {
        Span<char> separation = stackalloc char[Length];

        for (int i = 0; i < Length; ++i)
        {
            separation[i] = Symbol;
        }

        return new string(separation);
    }
}
