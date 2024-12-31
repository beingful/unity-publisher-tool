namespace Unity.Publisher.Tool.Infrastructure.Db.Operations;

public class OperationResult
{
    public readonly Exception? Exception;

    public OperationResult(Exception exception)
    {
        Exception = exception;
    }

    public bool Success => Exception == null;
}
