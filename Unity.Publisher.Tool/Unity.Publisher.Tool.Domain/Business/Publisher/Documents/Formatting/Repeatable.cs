namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public struct Repeatable
{
    public readonly char Symbol;
    public readonly int Length;

    public Repeatable(char symbol, int length)
    {
        Symbol = symbol;
        Length = length;

        String = ToString();
    }

    public string String { get; }

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
