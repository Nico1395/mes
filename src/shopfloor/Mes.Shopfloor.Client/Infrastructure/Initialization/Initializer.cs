using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

internal sealed class Initializer(IServiceProvider _serviceProvider) : IInitializer
{
    public async Task<IReadOnlyList<InitializationIssue>> InitializeAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var initializers = scope.ServiceProvider.GetServices<IInitializationJob>().OrderBy(j => j.Order).ToList();
        if (initializers.Count == 0)
            throw new InvalidOperationException("No terminal initializers were found.");

        var context = new InitializationContext();
        foreach (var initializer in initializers)
            await initializer.InitializeAsync(context, cancellationToken);

        return context.Issues;
    }
}