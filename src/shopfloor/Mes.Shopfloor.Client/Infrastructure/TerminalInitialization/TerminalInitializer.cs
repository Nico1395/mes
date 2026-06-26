using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Infrastructure.TerminalInitialization;

internal sealed class TerminalInitializer(IServiceProvider _serviceProvider) : ITerminalInitializer
{
    public async Task<IReadOnlyList<TerminalInitializationIssue>> InitializeAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var initializers = scope.ServiceProvider.GetServices<ITerminalInitializationJob>().OrderBy(j => j.Order).ToList();
        if (initializers.Count == 0)
            throw new InvalidOperationException("No terminal initializers were found.");

        var context = new TerminalInitializationContext();
        foreach (var initializer in initializers)
            await initializer.InitializeAsync(context, cancellationToken);

        return context.Issues;
    }
}