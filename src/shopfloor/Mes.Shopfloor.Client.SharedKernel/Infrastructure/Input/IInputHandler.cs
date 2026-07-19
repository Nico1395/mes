namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.Input;

public interface IInputHandler<in TRequest, out TInput>
    where TRequest : class, IInputRequest<TInput>
{
    TInput? RequestInput(TRequest request);
}