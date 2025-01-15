namespace Unity.Publisher.Tool.Domain.Notifications;

public sealed class Message
{
    public string? Subject { get; init; }

    public required string Body { get; init; }
}
