using Unity.Publisher.Tool.Http;
using Unity.Publisher.Tool.Notifications.Intefaces;

namespace Unity.Publisher.Tool.Notifications.Emails;

public class EmailNotificatorApiService
    : ApiService<EmailNotificatorApiService>, INotificatorApiService
{
    public EmailNotificatorApiService(
        IConfiguration configuration,
        ILogger<EmailNotificatorApiService> logger) : base(configuration, logger)
    {
    }

    public async Task SendAsync(Notification notification)
    {
        await PostAsync(
            request: new EmailNotification
            {
                Sender = notification.Sender,
                Recipient = notification.Recipient,
                Content = new EmailContent
                {
                    Message = notification.Content.Message
                },
                EmailServer = EmailServer.Gmail
            });
    }
}
