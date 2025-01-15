using System.Text;

namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting.Operations;

public class LineFeed : IFormattingOperation
{
    private readonly IParagraphFormattingOperation _indent;
    private readonly LineFeedCharacter _lineFeedCharacter;
    private readonly FormattingOptions _options;

    public LineFeed(IParagraphFormattingOperation indent, FormattingOptions options)
    {
        _indent = indent;
        _options = options;
        _lineFeedCharacter = new LineFeedCharacter();
    }

    public string Apply(params string[] paragraphs)
    {
        string[] formatted = new string[paragraphs.Length];

        for (int i = 0; i < formatted.Length; ++i)
        {
            formatted[i] = paragraphs[i].Length > _options.LineCapacity
                ? Apply(paragraphs[i])
                : paragraphs[i];
        }

        return string.Join('\n', formatted);
    }

    //private string Apply(string text)
    //{
    //    List<char> formatted = new(text.Length);

    //    StringBuilder word = new();

    //    int currentLineLength = 0;

    //    string indent = _indent.Apply(string.Empty);

    //    for (int i = 0, j = 0; j < text.Length; ++i)
    //    {
    //        if (char.IsWhiteSpace(text[j]))
    //        {
    //            if (currentLineLength + word.Length <= _options.LineCapacity)
    //            {
    //                formatted.InsertRange(i - word.Length, word.ToString());
    //            }
    //            else if (word.Length + indent.Length <= _options.LineCapacity)
    //            {
    //                formatted.InsertRange(i - word.Length, $"\n{indent}{word}");

    //                i += indent.Length + 1;
    //            }
    //            else
    //            {
    //                IEnumerable<char[]> wordChunks = word.ToString()
    //                    .Chunk(_options.LineCapacity - indent.Length);

    //                int leftToInsert = word.Length;

    //                foreach (char[] chunk in wordChunks)
    //                {
    //                    formatted.InsertRange(i - leftToInsert, $"\n{indent}{chunk}");

    //                    leftToInsert -= chunk.Length;
    //                    i += indent.Length + 1;
    //                }
    //            }
    //        }

    //        word.Append(text[j]);

    //        ++j;
    //    }

    //    return string.Join(string.Empty, formatted);
    //}

    private string Apply(string text)
    {
        List<char> formatted = new(text.Length);

        int charCounter = 0;

        for (int i = 0, j = 0; j < text.Length; ++i)
        {
            if (text[j] == '\n')
            {
                NewLineCase(text, formatted, ref charCounter, i, ref j);
            }
            else if (charCounter - 1 == _options.LineCapacity)
            {
                LineFeedCase(text, formatted, ref charCounter, ref i, ref j);
            }
            else
            {
                DefaultCase(text, formatted, ref charCounter, i, ref j);
            }
        }

        return string.Join(string.Empty, formatted);
    }

    private void NewLineCase(string text, List<char> result, ref int charCounter, int i, ref int j)
    {
        result.Insert(i, text[j]);

        charCounter = 0;
        ++j;
    }

    private void LineFeedCase(string text, List<char> result, ref int charCounter, ref int i, ref int j)
    {
        char lineFeedCharacter = j + 1 < text.Length
            ? _lineFeedCharacter.Get(text[j - 1], text[j], text[j + 1])
            : _lineFeedCharacter.Get(text[j - 1], text[j]);

        result.InsertRange(i, [lineFeedCharacter, '\n']);

        string indent = _indent.Apply(string.Empty);

        result.InsertRange(i + 2, indent);

        if (lineFeedCharacter == text[j])
        {
            ++j;
        }

        i += (indent.Length + 1);
        charCounter = 0;
    }

    private void DefaultCase(string text, List<char> result, ref int charCounter, int i, ref int j)
    {
        result.Insert(i, text[j]);

        ++j;
        ++charCounter;
    }
}
