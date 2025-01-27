namespace Unity.Publisher.Tool.Access.Options;

public class AuthenticationOptions
{
    public static string JsonKey => "Authentication";

    public required string ClientId { get; init; }

    public required string Secret { get; init; }

    public required string Issuer { get; init; }
}
