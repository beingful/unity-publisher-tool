using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.App.Services.Statement;

public class StatementUpdateEventInitiator : EventInitiator<PublisherStatement>
{
    public StatementUpdateEventInitiator(
        IEventDataProvider<PublisherStatement> dataProvider,
        IEventHandler<PublisherStatement> eventHandler)
        : base(dataProvider, eventHandler)
    {
    }

    protected override bool EventOccured(PublisherStatement data) => data.IsEmpty == false;
}
