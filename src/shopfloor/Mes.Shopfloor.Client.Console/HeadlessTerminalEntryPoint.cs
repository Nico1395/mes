using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Client.Infrastructure.Initialization;
using Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Console;

internal sealed class HeadlessTerminalEntryPoint : EntryPoint
{
    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        System.Console.WriteLine("Terminal initializing...");

        var initializer = Services.GetRequiredService<IInitializer>();
        var issues = await initializer.InitializeAsync(cancellationToken);

        var anyCriticalIssues = issues.Any(i => i.Severity == InitializationIssueSeverity.Critical);
        System.Console.WriteLine(anyCriticalIssues
            ? "Encountered at least one critical issue while initializing:"
            : "Encountered several issues while initializing:");

        foreach (var issue in issues)
            System.Console.WriteLine($"    1. {issue.Severity} - {issue.Message}");

        if (anyCriticalIssues)
        {
            System.Console.WriteLine("\nAt least one critical issue is preventing the application from continuing. Press any key to exit.");
            System.Console.ReadKey();
            return;
        }

        System.Console.WriteLine("Terminal initialized!");

        System.Console.WriteLine("\nBeginning production...");

        using var scope = Services.CreateScope();
        {
            var routine = scope.ServiceProvider.GetRequiredService<ITerminalRoutine>();
            await routine.ExecuteAsync(cancellationToken);
        }
        
        System.Console.ReadKey();
    }
}