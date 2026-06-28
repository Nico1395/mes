namespace Mes.Shopfloor.Client.Infrastructure.Input;

public interface IInputPromptHandler<in TRequest, out TInput>
    where TRequest : class, IInputPrompt<TInput>
{
    TInput? Prompt(TRequest request);
}