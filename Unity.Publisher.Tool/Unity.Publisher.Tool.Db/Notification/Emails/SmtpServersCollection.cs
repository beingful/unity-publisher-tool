using Unity.Publisher.Tool.Infrastructure.Notification.Emails.Models;

namespace Unity.Publisher.Tool.Infrastructure.Notification.Emails;

public sealed class SmtpServersCollection
{
    public IReadOnlyDictionary<EmailServer, SmtpServer> _emailServers;

    public SmtpServersCollection()
    {
        _emailServers = new Dictionary<EmailServer, SmtpServer>
        {
            { EmailServer.Gmail, new SmtpServer(Host: "smtp.gmail.com", Port: 587) },
            { EmailServer.Outlook, new SmtpServer(Host: "smtp.live.com", Port: 587) },
            { EmailServer.Office365, new SmtpServer(Host: "smtp.office365.com", Port: 587) },
            { EmailServer.Yahoo, new SmtpServer(Host: "smtp.mail.yahoo.com", Port: 465) },
            { EmailServer.YahooPlus, new SmtpServer(Host: "plus.smtp.mail.yahoo.com", Port: 465) },
            { EmailServer.Hotmail, new SmtpServer(Host: "smtp.live.com", Port: 465) },
            { EmailServer.Zoho, new SmtpServer(Host: "smtp.zoho.com", Port: 465) }
        };
    }

    public SmtpServer this[EmailServer emailServer]
    {
        get
        {
            return _emailServers[emailServer];
        }
    }
}
