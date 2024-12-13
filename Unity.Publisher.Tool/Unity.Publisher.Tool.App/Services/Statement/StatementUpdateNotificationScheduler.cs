using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Infrastructure.Db;
using static Unity.Publisher.Tool.App.Models.PublisherEvents;

namespace Unity.Publisher.Tool.App.Services.Statement;

public class StatementUpdateNotificationScheduler : EventNotificationScheduler<StatementUpdate, StatementUpdateEventInitiator>
{
    public StatementUpdateNotificationScheduler(
        EventScheduler<StatementUpdate, StatementUpdateEventInitiator> scheduler,
        IStorage dbRepository)
        : base(scheduler, dbRepository)
    {
    }

    public override async Task ScheduleAsync(DataTransferEndpoints data)
    {
        await ScheduleAsync(TriggerTime.FromTimeInterval(minutes: 1), data.Sender, data.Receiver);
    }
}
