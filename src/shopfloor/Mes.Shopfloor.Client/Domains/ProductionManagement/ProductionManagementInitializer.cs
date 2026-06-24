using Mes.Shopfloor.Terminal.Core.Configuration;
using Mes.Shopfloor.Terminal.Core.Domains.ProductionManagement.Subdomains.Resources.Manager;
using Mes.Shopfloor.Terminal.Core.Infrastructure;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Terminal.Core.Domains.ProductionManagement;

internal sealed class ProductionManagementInitializer(
    IOptions<ProductionUnitOptions> _options,
    IProductionUnitModelManager _productionUnitModelManager) : IInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var productionUnit = await _productionUnitModelManager.GetCurrentAsync(cancellationToken) ?? throw new InvalidOperationException($"No production unit for key '{_options.Value.Key}' could be fetched."); 

        // 2. Load current order
        
        // 3. Require employee to sign in
        
        // 4. Determine status or set to idle status
        
    }
}