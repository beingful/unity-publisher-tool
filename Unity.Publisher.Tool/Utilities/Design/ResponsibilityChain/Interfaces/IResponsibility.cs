namespace Utilities.Design.ResponsibilityChain.Interfaces;

public interface IResponsibility<TTarget>
{
    Task<TTarget> DoAsync(TTarget target);
}