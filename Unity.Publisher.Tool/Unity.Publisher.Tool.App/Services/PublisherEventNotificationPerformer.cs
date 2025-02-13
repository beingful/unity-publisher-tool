using Microsoft.Extensions.Logging;
using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Publisher;
using Unity.Publisher.Tool.Domain.Publisher.Services.Reports;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public abstract class PublisherEventNotificationPerformer : INoRetryWorker<NotificationDetails>
{
    private readonly PublisherEvent _publisherEvent;
    private readonly IPublisherMessageService _messageService;
    private readonly ILogger _logger;

    public PublisherEventNotificationPerformer(
        PublisherEvent publisherEvent,
        IPublisherMessageService messageService,
        ILogger<PublisherEventNotificationPerformer> logger)
    {
        _publisherEvent = publisherEvent;
        _messageService = messageService;
        _logger = logger;
    }

    public async Task ExecuteAsync(NotificationDetails notificationDetails)
    {
        CancellationTokenSource cancellationTokenSource = new();

        try
        {
            _logger.LogInformation($"{_publisherEvent} event execution started.");

            await _messageService.MessageAsync(notificationDetails.Sender,
                notificationDetails.Receiver, cancellationTokenSource.Token);

            _logger.LogInformation($"{_publisherEvent} event execution ended.");
        }
        catch (Exception exception)
        {
            await cancellationTokenSource.CancelAsync();

            cancellationTokenSource.Dispose();

            _logger.LogError(exception, $"{_publisherEvent} event execution failed.");

            throw;
        }
    }
}
