namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement.Repositories;

internal interface IWorkerModelRepository
{
    Task<WorkerModel?> GetByNumberAsync(string workerNumber, CancellationToken cancellationToken);
}