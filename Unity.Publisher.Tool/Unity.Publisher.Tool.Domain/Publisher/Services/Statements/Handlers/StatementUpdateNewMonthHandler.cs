namespace Unity.Publisher.Tool.Domain.Publisher.Services.Statements.Handlers;

internal class StatementUpdateNewMonthHandler : StatementUpdateBaseHandler
{
    public StatementUpdateNewMonthHandler(IStatementUpdateHandler? next = null) : base(next)
    {
    }

    public override PublisherStatement Handle(PublisherStatement newStatement, PublisherStatement lastStatement)
    {
        if (newStatement.CreationTime.Month != lastStatement.CreationTime.Month)
        {
            lastStatement = PublisherStatement.Empty();
        }
        
        return base.Handle(newStatement, lastStatement);
    }
}
