using Unity.Publisher.Tool.Domain.Notifications;

namespace Unity.Publisher.Tool.Infrastructure.Notification.Emails.Services;

public class SimpleEmailNotificator : INotificator
{
    private readonly INotificator<EmailNotification> _emailNotificator;

    public SimpleEmailNotificator(INotificator<EmailNotification> emailNotificator)
    {
        _emailNotificator = emailNotificator;
    }

    public Task SendAsync(Sender sender, Receiver receiver, Message message, CancellationToken cancellationToken = default)
    {
        EmailNotification email = new()
        {
            Sender = sender,
            Recipient = receiver,
            Content = new EmailContent
            {
                Message = message
            }
        };

        return _emailNotificator.SendAsync(email, cancellationToken);
    }
}
