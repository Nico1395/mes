using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mes.Shopfloor.Client.Console.Startup;

public sealed class ConsoleAppBuilder
{
    public ConsoleAppBuilder() : this(args: null)
    {
    }

    public ConsoleAppBuilder(string[]? args)
    {
        Services = new ServiceCollection();
        Configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json")
            .AddCommandLine(args ?? [])
            .Build();

        Services.AddSingleton(Configuration);
    }

    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; } 
    public ILoggingBuilder Logging { get; }

    public ConsoleAppBuilder UseEntryPoint<TEntryPoint>()
        where TEntryPoint : EntryPoint
    {
        Services.AddSingleton<EntryPoint, TEntryPoint>();
        return this;
    }

    public ConsoleApp Build() => new(Services, Configuration);
}