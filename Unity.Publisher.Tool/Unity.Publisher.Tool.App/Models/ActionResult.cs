using Unity.Publisher.Tool.Domain.Publisher;

namespace Unity.Publisher.Tool.App.Models;

public class ActionResult
{
    public required PublisherEvent Event { get; init; }

    public required Result Result { get; init; }

    public string? Reason { get; init; }

    public static ActionResult OnSuccess(PublisherEvent publisherEvent)
    {
        return new ActionResult
        {
            Event = publisherEvent,
            Result = Result.Success
        };
    }

    public static ActionResult OnFail(PublisherEvent publisherEvent, string? reason)
    {
        return new ActionResult
        {
            Event = publisherEvent,
            Result = Result.Fail,
            Reason = reason
        };
    }
}
