namespace Unity.Publisher.Tool.Domain.Business.Documents.Formatting;

public class FormattingOptions
{
    public int Padding { get; set; } = 0;

    public char? Separator { get; set; } = null;

    public int LineCapacity { get; set; } = 100;
}
