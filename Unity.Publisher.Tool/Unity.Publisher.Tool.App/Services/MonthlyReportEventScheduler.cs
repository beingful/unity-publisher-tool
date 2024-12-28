using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;

namespace Unity.Publisher.Tool.App.Services;

public class MonthlyReportEventScheduler : PublisherEventNotificationScheduler
{
    private readonly IProvider<string> _idProvider;

    public MonthlyReportEventScheduler(IDataDrivenScheduler scheduler) : base(scheduler)
    {
        _idProvider = new PublisherEventIdProvider(PublisherEvent.MonthlyReport);
    }

    public override async Task ScheduleAsync(NotificationJobData data)
    {
        Job job = new(id: _idProvider.Provide(), time: TriggerTime.Monthly());

        //await ScheduleAsync<PublisherReport>(job, data);
    }

    public override async Task UnscheduleAsync()
    {
        await UnscheduleAsync(jobId: _idProvider.Provide());
    }
}
