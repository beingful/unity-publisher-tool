using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public interface IPublisherReportingService
{
    Task ReportAsync(Sender sender, Receiver receiver, CancellationToken cancellationToken = default);
}
