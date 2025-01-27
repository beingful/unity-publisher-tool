namespace Unity.Publisher.Tool.Access;

public class Admin
{
    private const char _emailsSeparator = ';';

    public Admin(string emails)
    {
        Emails = emails.Split(_emailsSeparator);
    }

    public string[] Emails { get; }
}
