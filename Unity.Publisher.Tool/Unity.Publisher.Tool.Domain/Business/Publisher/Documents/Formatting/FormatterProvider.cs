using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data.Providers;

namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

public class FormatterProvider : IKeyedProvider<Type, IFormatter>
{
    public Dictionary<Type, IFormatter> _formatters;

    public FormatterProvider()
    {
        _formatters = new Dictionary<Type, IFormatter>
        {
            {
                typeof(PublisherStatement),
                new Formatter(
                    options: new FormattingOptions(separatingCharacter: '-'))
            }
        };
    }

    public IFormatter Provide(Type key)
    {
        return _formatters.TryGetValue(key, out IFormatter? result)
            ? result
            : new Formatter();
    }
}
