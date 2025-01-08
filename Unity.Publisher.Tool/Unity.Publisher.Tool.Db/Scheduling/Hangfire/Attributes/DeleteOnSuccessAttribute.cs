using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace Unity.Publisher.Tool.Infrastructure.Scheduling.Hangfire.Attributes;

public class DeleteOnSuccessAttribute : JobFilterAttribute, IApplyStateFilter
{
    private readonly TimeSpan _deleteAfter;

    public DeleteOnSuccessAttribute(int hours = 0, int minutes = 0, int seconds = 0)
    {
        _deleteAfter = new TimeSpan(hours, minutes, seconds);
    }

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        if (context.NewState.Name == SucceededState.StateName)
        {
            context.JobExpirationTimeout = _deleteAfter;
        }
    }

    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
    }
}
