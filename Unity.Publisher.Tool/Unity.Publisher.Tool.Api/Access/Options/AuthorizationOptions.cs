namespace Unity.Publisher.Tool.Access.Options;

public class AuthorizationOptions
{
    public static string JsonKey => "Authorization";

    public required Admin Admin { get; init; }
}
