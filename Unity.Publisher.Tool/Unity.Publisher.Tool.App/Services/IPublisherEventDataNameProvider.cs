using Unity.Publisher.Tool.Domain.Data;

namespace Unity.Publisher.Tool.App.Services;

public interface IPublisherEventDataNameProvider<TIn> : IProvider<TIn, string>
{
}
