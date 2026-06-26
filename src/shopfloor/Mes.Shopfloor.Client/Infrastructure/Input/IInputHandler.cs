namespace Mes.Shopfloor.Client.Infrastructure.Input;

public interface IInputHandler<in TRequest, out TInput>
    where TRequest : class, IInputRequest<TInput>
{
    TInput? RequestInput(TRequest request);
}