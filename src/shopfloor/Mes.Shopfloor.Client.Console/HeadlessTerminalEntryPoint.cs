using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Terminal.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Console;

internal sealed class HeadlessTerminalEntryPoint : EntryPoint
{
    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        System.Console.WriteLine("[0] Terminal initializing...");

        var initializers = Services.GetServices<IInitializer>().ToList();
        if (initializers.Count == 0)
            throw new InvalidOperationException($"No terminal initializers were found.");

        foreach (var initializer in initializers)
            await initializer.InitializeAsync(cancellationToken);

        System.Console.WriteLine("[0] Terminal initialized!");

        System.Console.WriteLine("[1] Beginning production...");
        
        // TODO -> Allow entering quantities or automatically generate quantities and statuses
        
        System.Console.ReadLine();
    }
}