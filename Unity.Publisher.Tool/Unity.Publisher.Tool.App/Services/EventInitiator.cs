using Unity.Publisher.Tool.Infrastructure.Scheduling;

namespace Unity.Publisher.Tool.App.Services;

public abstract class EventInitiator<TData> : IScheduleWorker
{
    private readonly IEventDataProvider<TData> _dataProvider;
    private readonly IEventHandler<TData> _eventHandler;

    public EventInitiator(
        IEventDataProvider<TData> dataProvider,
        IEventHandler<TData> eventHandler)
    {
        _dataProvider = dataProvider;
        _eventHandler = eventHandler;
    }

    public async Task ExecuteAsync()
    {
        await InitiateAsync();
    }

    public async Task InitiateAsync()
    {
        TData data = await _dataProvider.ProvideAsync();

        if (EventOccured(data))
        {
            await _eventHandler.HandleAsync(data);
        }
    }

    protected abstract bool EventOccured(TData data);
}
