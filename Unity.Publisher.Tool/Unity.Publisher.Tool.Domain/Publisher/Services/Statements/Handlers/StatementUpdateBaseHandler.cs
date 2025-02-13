namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public abstract class StatementUpdateBaseHandler : IStatementUpdateHandler
{
    private IStatementUpdateHandler? _next;

    public StatementUpdateBaseHandler(IStatementUpdateHandler? next)
    {
        _next = next;
    }

    public virtual PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement)
    {
        return _next?.Handle(lastStatement, newStatement) ?? PublisherStatement.Empty();
    }
}
