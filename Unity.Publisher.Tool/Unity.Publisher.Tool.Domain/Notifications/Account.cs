namespace Unity.Publisher.Tool.Domain.Notifications;

public sealed class Account
{
    public required string Id { get; init; }

    public required string Secret { get; init; }
}
