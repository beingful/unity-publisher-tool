using Unity.Publisher.Tool.App.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Models;
using Unity.Publisher.Tool.Domain.Data;
using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateEventScheduler : PublisherEventNotificationScheduler
{
    private readonly IProvider<string> _idProvider;

    public StatementUpdateEventScheduler(IDataDrivenScheduler scheduler) : base(scheduler)
    {
        _idProvider = new PublisherEventIdProvider(PublisherEvent.StatementUpdate);
    }

    public override async Task ScheduleAsync(NotificationJobData data)
    {
        Job job = new(id: _idProvider.Provide(), time: TriggerTime.FromTimeInterval(minutes: 1));

        await ScheduleAsync<PublisherStatement>(job, data);
    }

    public override async Task UnscheduleAsync()
    {
        await UnscheduleAsync(jobId: _idProvider.Provide());
    }
}
