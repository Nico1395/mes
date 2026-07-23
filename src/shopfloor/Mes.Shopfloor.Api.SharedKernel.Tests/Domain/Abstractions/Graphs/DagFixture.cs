using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Api.SharedKernel.Tests.Domain.Abstractions.Graphs;

public sealed class DagFixture : IServiceProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    public DagFixture()
    {
        var services = new ServiceCollection();

        

        _serviceProvider = services.BuildServiceProvider();
    }

    public object? GetService(Type serviceType)
    {
        return _serviceProvider.GetService(serviceType);
    }
}