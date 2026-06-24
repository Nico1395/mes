using Microsoft.Extensions.Configuration;

namespace Mes.Shopfloor.Client.Console.Startup;

public abstract class EntryPoint : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private IServiceProvider? _services;
    private IConfiguration? _configuration;

    protected internal CancellationToken CancellationToken => _cancellationTokenSource.Token;

    protected internal IServiceProvider Services
    {
        get => _services ?? throw new InvalidOperationException($"Entry point not initialized yet.");
        internal set => _services = value;
    }

    protected internal IConfiguration Configuration
    {
        get => _configuration ?? throw new InvalidOperationException($"Entry point not initialized yet.");
        internal set => _configuration = value;
    }

    public abstract Task RunAsync(CancellationToken cancellationToken);

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}