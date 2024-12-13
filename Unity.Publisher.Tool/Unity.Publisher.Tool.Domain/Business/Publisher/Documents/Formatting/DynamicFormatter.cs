using Unity.Publisher.Tool.Domain.Data.Providers;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public class DynamicFormatter<TModel> : IFormatter<TModel>
{
    private readonly IFormatter _formatter;

    public DynamicFormatter(IKeyedProvider<Type, IFormatter> formatterProvider)
    {
        _formatter = formatterProvider.Provide(typeof(TModel));
    }

    public string ContentSeparator => _formatter.ContentSeparator;

    public FormattingOptions Options => _formatter.Options;

    public void SetMargin(int margin)
    {
        _formatter.SetMargin(margin);
    }

    public string FormatLines(params string[] lines)
    {
        return _formatter.FormatLines(lines);
    }

    public string FormatLine(string line)
    {
        return _formatter.FormatLine(line);
    }
}
