namespace Unity.Publisher.Tool.Domain.Business.Models;

public sealed class Loss
{
    public readonly int Refund;

    public readonly int Chargeback;

    public Loss(int refund, int chargeback)
    {
        Refund = refund;
        Chargeback = chargeback;

        Total = Refund + Chargeback;
    }

    public int Total { get; }
}
