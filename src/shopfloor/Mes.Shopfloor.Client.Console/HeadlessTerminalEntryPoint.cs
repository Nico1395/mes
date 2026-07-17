using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalInitialization;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Console;

internal sealed class HeadlessTerminalEntryPoint(IMessagePublisher messagePublisher) : EntryPoint
{
    public override async Task RunAsync(CancellationToken cancellationToken)
    {
        System.Console.WriteLine("Terminal initializing...");

        var initializer = Services.GetRequiredService<ITerminalInitializer>();
        var issues = await initializer.InitializeAsync(cancellationToken);

        var anyCriticalIssues = issues.Any(i => i.Severity == TerminalInitializationIssueSeverity.Critical);
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

        var wentOnline = new ProductionUnitWentOnlineV1();

        // All services related to the terminal routine are registered as scoped so this should execute in its very own scope
        using var scope = Services.CreateScope();
        {
            var routine = scope.ServiceProvider.GetRequiredService<ITerminalRoutine>();
            await routine.ExecuteAsync(cancellationToken);
        }
        
        System.Console.ReadKey();
    }
}