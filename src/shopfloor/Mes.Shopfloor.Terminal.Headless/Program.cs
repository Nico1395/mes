using Mes.Shopfloor.Terminal.Core;
using Mes.Shopfloor.Terminal.Headless.Startup;

namespace Mes.Shopfloor.Terminal.Headless;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConsoleAppBuilder(args).UseEntryPoint<HeadlessTerminalEntryPoint>();

        builder.Services.AddTerminal(builder.Configuration);
        
        await builder.Build().RunAsync();
    }
}