namespace Unity.Publisher.Tool.Access.Options;

public class GoogleAuthenticationOptions : AuthenticationOptions
{
    public static new string JsonKey => $"{AuthenticationOptions.JsonKey}:Google";
}
