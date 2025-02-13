namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

public abstract class StatementUpdateBaseHandler : IStatementUpdateHandler
{
    private readonly IStatementUpdateHandler? _next;

    public StatementUpdateBaseHandler(IStatementUpdateHandler? next)
    {
        _next = next;
    }

    public virtual PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement)
    {
        return _next?.Handle(newStatement, lastStatement) ?? PublisherStatement.Empty();
    }
}
