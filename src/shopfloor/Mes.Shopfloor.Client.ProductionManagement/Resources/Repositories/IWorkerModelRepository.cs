namespace Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;

internal interface IWorkerModelRepository
{
    Task<WorkerModel?> GetByNumberAsync(string workerNumber, CancellationToken cancellationToken);
}