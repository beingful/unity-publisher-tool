namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.Options;

public class PublisherAccountOptions
{
    public static string JsonKey => $"{PublisherApiOptions.JsonKey}:Account";

    public required string Email { get; init; }

    public required string Password { get; init; }
}
