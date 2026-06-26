namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling;

internal enum ProductionOrderState
{
    Defined = 0,
    Scheduled = 1,
    Paused = 2,
    Completed = 3,
}