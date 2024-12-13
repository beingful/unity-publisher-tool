using Unity.Publisher.Tool.Infrastructure.Notification.Emails.Models;
using Unity.Publisher.Tool.Infrastructure.Notification.Models;

namespace Unity.Publisher.Tool.Infrastructure.Notification.Emails;

public class SimpleEmailNotificator : INotificator
{
    private readonly INotificator<EmailNotification> _emailNotificator;

    public SimpleEmailNotificator(INotificator<EmailNotification> emailNotificator)
    {
        _emailNotificator = emailNotificator;
    }

    public async Task SendAsync(Sender sender, Receiver receiver, Message message, CancellationToken cancellationToken)
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

        await _emailNotificator.SendAsync(email, CancellationToken.None).ConfigureAwait(false);
    }
}
