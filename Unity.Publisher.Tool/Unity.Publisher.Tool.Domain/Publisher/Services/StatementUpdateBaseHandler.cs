namespace Unity.Publisher.Tool.Domain.Publisher.Services;

public abstract class StatementUpdateBaseHandler : IStatementUpdateHandler
{
    private IStatementUpdateHandler? _next;

    public StatementUpdateBaseHandler(IStatementUpdateHandler? next)
    {
        _next = next;
    }

    public virtual PublisherStatement Handle(PublisherStatement lastStatement, PublisherStatement newStatement)
    {
        return _next?.Handle(lastStatement, newStatement) ?? PublisherStatement.Empty();
    }
}
