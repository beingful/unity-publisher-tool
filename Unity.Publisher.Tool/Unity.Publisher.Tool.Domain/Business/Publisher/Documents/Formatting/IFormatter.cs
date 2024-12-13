namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public interface IFormatter
{
    string ContentSeparator { get; }

    FormattingOptions Options { get; }

    public void SetMargin(int margin);

    string FormatLines(params string[] lines);

    string FormatLine(string line);
}

public interface IFormatter<TDocument> : IFormatter;
