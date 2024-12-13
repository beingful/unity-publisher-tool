using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.App.Services.Report;

public class MonthlyReportEventInitiator : EventInitiator<PublisherReport>
{
    public MonthlyReportEventInitiator(
        IEventDataProvider<PublisherReport> dataProvider,
        IEventHandler<PublisherReport> eventHandler)
        : base(dataProvider, eventHandler)
    {
    }

    protected override bool EventOccured(PublisherReport data) => true;
}
