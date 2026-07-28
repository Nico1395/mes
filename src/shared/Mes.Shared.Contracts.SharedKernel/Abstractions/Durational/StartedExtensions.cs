namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Durational;

public static class StartedExtensions
{
    public static bool StartedAfter(this IStarted started, IEnded ended)
    {
        if (!ended.EndedAt.HasValue)
            return false;

        return ended.EndedAt.Value <= started.StartedAt;
    }
}