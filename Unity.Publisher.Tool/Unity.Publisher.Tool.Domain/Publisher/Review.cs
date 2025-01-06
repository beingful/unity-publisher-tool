namespace Unity.Publisher.Tool.Domain.Publisher;

public sealed class Review
{
    public readonly long Id;

    public readonly string Product;

    public readonly string Subject;

    public readonly string Body;

    public readonly float Rating;

    public readonly DateTime Created;

    public Review(long id, string product, string subject,
        string body, int rating, DateTime created)
    {
        Id = id;
        Product = product;
        Subject = subject;
        Body = body;
        Rating = rating;
        Created = created;
    }
}
