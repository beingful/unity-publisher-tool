using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.App.Models;

public static class PublisherEvents
{
    private static IReadOnlyDictionary<Type, Event> _events;

    static PublisherEvents()
    {
        _events = new Dictionary<Type, Event>()
        {
            { typeof(PublisherStatement), new StatementUpdate() },
            { typeof(PublisherReport), new MonthlyReport() }
        };
    }

    public class StatementUpdate : Event
    {
        public override PublisherEvent Type => PublisherEvent.StatementUpdate;

        public override string DisplayName => "Update";
    }

    public class MonthlyReport : Event
    {
        public override PublisherEvent Type => PublisherEvent.MonthlyReport;

        public override string DisplayName => "Report";
    }

    public abstract class Event
    {
        public abstract PublisherEvent Type { get; }

        public abstract string DisplayName { get; }

        public string Name => Enum.GetName(Type)!;
    }

    public static Event ResolveFor<TData>()
    {
        return _events[typeof(TData)];
    }
}
