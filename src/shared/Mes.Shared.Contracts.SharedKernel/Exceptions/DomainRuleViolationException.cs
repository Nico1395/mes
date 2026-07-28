namespace Mes.Shared.Contracts.SharedKernel.Exceptions;

public sealed class DomainRuleViolationException(string message) : Exception(message)
{
    public static DomainRuleViolationException Create<T>(string explanation)
    {
        return new DomainRuleViolationException($"Domain rule violated for '{typeof(T)}': {explanation}");
    }

    public static DomainRuleViolationException Create(string explanation)
    {
        return new DomainRuleViolationException($"Domain rule violated: {explanation}");
    }

    public static void Throw<T>(string explanation)
    {
        throw Create<T>(explanation);
    }

    public static void Throw(string explanation)
    {
        throw Create(explanation);
    }

    public static void ThrowIf<T>(bool condition, string explanation)
    {
        throw Create<T>(explanation);
    }

    public static void ThrowIf(bool condition, string explanation)
    {
        throw Create(explanation);
    }
}