using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationPerformer : INoRetryWorker<NotificationDetails>
{
    private readonly PublisherEvent _publisherEvent;
    private readonly IPublisherReportingService _reportingService;
    private readonly ILogger _logger;

    public PublisherEventNotificationPerformer(
        PublisherEvent publisherEvent,
        IPublisherReportingService reportingService,
        ILogger<PublisherEventNotificationPerformer> logger)
    {
        _publisherEvent = publisherEvent;
        _reportingService = reportingService;
        _logger = logger;
    }

    public async Task ExecuteAsync(NotificationDetails notificationDetails)
    {
        try
        {
            _logger.LogInformation($"{_publisherEvent} event execution started.");

            await _reportingService.ReportAsync(notificationDetails.Sender, notificationDetails.Receiver);

            _logger.LogInformation($"{_publisherEvent} event execution ended.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{_publisherEvent} event execution failed.");

            throw;
        }
    }
}
