using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Domain.Publisher.Services.Reports;

public interface IPublisherMessageService
{
    Task MessageAsync(Sender sender, Receiver receiver, CancellationToken cancellationToken = default);
}
