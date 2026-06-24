using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Headless.Startup;

public sealed class ConsoleApp(IServiceCollection services, IConfiguration configuration)
{
    private EntryPoint? EntryPoint { get; set; }

    public IServiceProvider Services { get; } = services.BuildServiceProvider();
    public IConfiguration Configuration { get; } = configuration;

    public Task RunAsync()
    {
        EntryPoint ??= Services.GetRequiredService<EntryPoint>();

        EntryPoint.Services = Services;
        EntryPoint.Configuration = Configuration;

        try
        {
            return EntryPoint.RunAsync(EntryPoint.CancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine("The application ran into a critical error and can't recover.");
            Console.WriteLine(ex);
            
            Console.Write("\nPress any key to exit the application...");
            Console.ReadLine();
            
            return Task.CompletedTask;
        }
    }
}