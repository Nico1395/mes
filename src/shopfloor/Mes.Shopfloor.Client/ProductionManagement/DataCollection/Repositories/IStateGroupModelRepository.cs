namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal interface IStateGroupModelRepository
{
    Task<StateGroupModel?> GetByIdAsync(Guid stateGroupId, CancellationToken cancellationToken);
}