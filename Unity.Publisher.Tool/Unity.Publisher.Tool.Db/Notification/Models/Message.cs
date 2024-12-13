namespace Unity.Publisher.Tool.Infrastructure.Notification.Models;

public sealed class Message
{
    public string? Subject { get; init; }

    public required string Body { get; init; }
}
