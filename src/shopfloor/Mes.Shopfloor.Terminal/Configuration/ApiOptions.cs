namespace Mes.Shopfloor.Terminal.Core.Configuration;

public sealed class ApiOptions
{
    public string? InventoryManagementUrl { get; set; }
    public string? MaintenanceManagementUrl { get; set; }
    public string? ProductionManagementUrl { get; set; }
    public string? QualityManagementUrl { get; set; }
}