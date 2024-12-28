using Unity.Publisher.Tool.Domain.Business.Publisher.Models;
using Unity.Publisher.Tool.Infrastructure.Scheduling.Workers;

namespace Unity.Publisher.Tool.App.Services;

public class StatementUpdateEventHandler : IScheduleWorker
{
    private readonly IScheduleWorker<PublisherStatement> _scheduleWorker;

    public StatementUpdateEventHandler(IScheduleWorker<PublisherStatement> scheduleWorker)
    {
        _scheduleWorker = scheduleWorker;
    }

    public async Task ExecuteAsync()
    {
        await _scheduleWorker.ExecuteAsync();
    }
}
