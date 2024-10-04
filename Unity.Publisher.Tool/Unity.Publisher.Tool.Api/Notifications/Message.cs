namespace Unity.Publisher.Tool.Notifications;

public sealed class Message
{
    public string? Subject { get; init; }

    public required string Body { get; init; }
}
