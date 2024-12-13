using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Db;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using static Unity.Publisher.Tool.App.Models.PublisherEvents;

namespace Unity.Publisher.Tool.App.Services.Report;

public class MonthlyReportNotificationScheduler : EventNotificationScheduler<MonthlyReport, MonthlyReportEventInitiator>
{
    public MonthlyReportNotificationScheduler(
        EventScheduler<MonthlyReport, MonthlyReportEventInitiator> scheduler,
        IStorage dbRepository)
        : base(scheduler, dbRepository)
    {
    }

    public override async Task ScheduleAsync(DataTransferEndpoints data)
    {
        await ScheduleAsync(TriggerTime.Monthly(), data.Sender, data.Receiver);
    }
}
