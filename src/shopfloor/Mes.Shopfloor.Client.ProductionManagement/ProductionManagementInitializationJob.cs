using Mes.Shopfloor.Client.Configuration;
using Mes.Shopfloor.Client.Infrastructure.Initialization;
using Mes.Shopfloor.Client.Infrastructure.Input;
using Mes.Shopfloor.Client.Infrastructure.Routine;
using Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Resources;
using Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.ProductionManagement;

internal sealed class ProductionManagementInitializationJob(
    IOptions<ProductionUnitOptions> _options,
    IRoutineContext _routineContext,
    IInputHandler<WorkerSignInInputRequest, string> _workerSignIn,
    IRejectGroupModelRepository _rejectGroupModelRepository,
    IStateGroupModelRepository _stateGroupModelRepository,
    IProductionUnitModelRepository _productionUnitModelRepository,
    IProductionUnitScheduleModelRepository _productionUnitScheduleModelRepository,
    IProductionProcessModelRepository _productionProcessModelRepository,
    IProductionOrderModelRepository _productionOrderModelRepository,
    IWorkerModelRepository _workerModelRepository) : IInitializationJob
{
    public int Order => 0;

    public async Task InitializeAsync(InitializationContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.Value.Key))
        {
            context.ReportIssue(InitializationIssueSeverity.Critical, "Production unit key not configured.");
            return;
        }

        var currentProductionUnit = await _productionUnitModelRepository.GetByKeyAsync(_options.Value.Key, cancellationToken);
        if (currentProductionUnit == null)
        {
            context.ReportIssue(InitializationIssueSeverity.Critical, $"No production unit for key '{_options.Value.Key}' could be fetched.");
            return;
        }

        _routineContext.SetIfNotNull(RoutineDataKey.ProductionUnit, currentProductionUnit);
        _routineContext.SetIfNotNull(RoutineDataKey.ProductionUnitId, currentProductionUnit.Id);

        var rejectGroup = await _rejectGroupModelRepository.GetByIdAsync(currentProductionUnit.Group.RejectGroupId, cancellationToken);
        _routineContext.SetIfNotNull(RoutineDataKey.RejectGroup, rejectGroup);
        _routineContext.SetIfNotNull(RoutineDataKey.RejectGroupId, rejectGroup?.Id);
        
        var stateGroup = await _stateGroupModelRepository.GetByIdAsync(currentProductionUnit.Group.StateGroupId, cancellationToken);
        _routineContext.SetIfNotNull(RoutineDataKey.StateGroup, stateGroup);
        _routineContext.SetIfNotNull(RoutineDataKey.StateGroupId, stateGroup?.Id);

        var currentSchedule = await _productionUnitScheduleModelRepository.GetByProductionUnitIdAsync(currentProductionUnit.Id, cancellationToken);
        _routineContext.SetIfNotNull(RoutineDataKey.Schedule, currentSchedule);
        _routineContext.SetIfNotNull(RoutineDataKey.ScheduleId, currentSchedule?.Id);

        var productionProcessId = currentSchedule?.GetCurrentTask()?.ProductionScheduleId;
        if (productionProcessId.HasValue)
        {
            var productionProcess = await _productionProcessModelRepository.GetByIdAsync(productionProcessId.Value, cancellationToken);
            _routineContext.SetIfNotNull(RoutineDataKey.ProductionProcess, productionProcess);
            _routineContext.SetIfNotNull(RoutineDataKey.ProductionProcessId, productionProcess?.Id);
        }

        var orderId = currentSchedule?.GetCurrentTask()?.ProductionOrderId;
        if (orderId.HasValue)
        {
            var order = await _productionOrderModelRepository.GetByIdAsync(orderId.Value, cancellationToken);
            _routineContext.SetIfNotNull(RoutineDataKey.Order, order);
            _routineContext.SetIfNotNull(RoutineDataKey.OrderId, order?.Id);
        }

        WorkerModel? worker = null;
        while (worker == null)
        {
            var workerNumber = _workerSignIn.RequestInput(new WorkerSignInInputRequest());
            worker = await _workerModelRepository.GetByNumberAsync(workerNumber ?? string.Empty, cancellationToken);
            if (worker == null)
                Console.WriteLine($"No worker for number '{workerNumber}' could be found. Try again.");
        }
        _routineContext.SetIfNotNull(RoutineDataKey.Worker, worker);
        _routineContext.SetIfNotNull(RoutineDataKey.WorkerId, worker.Id);
        _routineContext.SetIfNotNull(RoutineDataKey.WorkerNumber, worker.Number);
    }
}