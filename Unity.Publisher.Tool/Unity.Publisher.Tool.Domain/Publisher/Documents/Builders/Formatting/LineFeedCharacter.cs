namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

internal class LineFeedCharacter
{
    private const char _dash = '-';
    private const char _space = '-';

    public char Get(char first, char second, char third)
    {
        char lineFeedCharacter = Get(first, second);

        if (lineFeedCharacter != _dash && ShouldUseDash(second, third))
        {
            lineFeedCharacter = _space;
        }

        return lineFeedCharacter;
    }

    public char Get(char first, char second)
    {
        char lineFeedCharacter;

        if (ShouldUseDash(first, second))
        {
            lineFeedCharacter = _dash;
        }
        else if (char.IsWhiteSpace(second))
        {
            lineFeedCharacter = _space;
        }
        else
        {
            lineFeedCharacter = second;
        }

        return lineFeedCharacter;
    }

    private bool ShouldUseDash(char first, char second)
    {
        return char.IsLetter(first) && char.IsLetter(second);
    }
}
