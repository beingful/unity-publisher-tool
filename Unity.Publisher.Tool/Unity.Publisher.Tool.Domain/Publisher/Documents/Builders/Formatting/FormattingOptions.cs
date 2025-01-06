namespace Unity.Publisher.Tool.Domain.Publisher.Documents.Builders.Formatting;

public class FormattingOptions
{
    public int Padding { get; set; } = 0;

    public char? Separator { get; set; } = null;

    public int LineCapacity { get; set; } = 40;
}
