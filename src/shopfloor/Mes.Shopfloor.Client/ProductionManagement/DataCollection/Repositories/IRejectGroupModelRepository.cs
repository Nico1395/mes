namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal interface IRejectGroupModelRepository
{
    Task<RejectGroupModel?> GetByIdAsync(Guid rejectGroupId, CancellationToken cancellationToken);
}