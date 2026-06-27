using Mes.Shopfloor.Client.Configuration;
using Mes.Shopfloor.Client.Infrastructure.Input;
using Mes.Shopfloor.Client.Infrastructure.TerminalInitialization;
using Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Resources;
using Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.ProductionManagement;

internal sealed class ProductionManagementTerminalInitializationJob(
    IOptions<ProductionUnitOptions> _options,
    ITerminalRoutineContext terminalRoutineContext,
    IInputHandler<WorkerSignInInputRequest, string> _workerSignIn,
    IRejectGroupModelRepository _rejectGroupModelRepository,
    IStateGroupModelRepository _stateGroupModelRepository,
    IProductionUnitModelRepository _productionUnitModelRepository,
    IProductionUnitScheduleModelRepository _productionUnitScheduleModelRepository,
    IProductionProcessModelRepository _productionProcessModelRepository,
    IProductionOrderModelRepository _productionOrderModelRepository,
    IWorkerModelRepository _workerModelRepository) : ITerminalInitializationJob
{
    public int Order => 0;

    public async Task InitializeAsync(TerminalInitializationContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.Value.Key))
        {
            context.ReportIssue(TerminalInitializationIssueSeverity.Critical, "Production unit key not configured.");
            return;
        }

        // Fetching the production unit
        var currentProductionUnit = await _productionUnitModelRepository.GetByKeyAsync(_options.Value.Key, cancellationToken);
        if (currentProductionUnit == null)
        {
            context.ReportIssue(TerminalInitializationIssueSeverity.Critical, $"No production unit for key '{_options.Value.Key}' could be fetched.");
            return;
        }
        else
        {
            terminalRoutineContext.SetIfNotNull(DataKey.ProductionUnit, currentProductionUnit);
            terminalRoutineContext.SetIfNotNull(DataKey.ProductionUnitId, currentProductionUnit.Id);
        }

        // Fetching the reject group (and its rejects)
        var rejectGroup = await _rejectGroupModelRepository.GetByIdAsync(currentProductionUnit.Group.RejectGroupId, cancellationToken);
        terminalRoutineContext.SetIfNotNull(DataKey.RejectGroup, rejectGroup);
        terminalRoutineContext.SetIfNotNull(DataKey.RejectGroupId, rejectGroup?.Id);
        
        // Fetch the state group (and its states)
        var stateGroup = await _stateGroupModelRepository.GetByIdAsync(currentProductionUnit.Group.StateGroupId, cancellationToken);
        terminalRoutineContext.SetIfNotNull(DataKey.StateGroup, stateGroup);
        terminalRoutineContext.SetIfNotNull(DataKey.StateGroupId, stateGroup?.Id);

        // Fetch the schedule (and its task)
        var currentSchedule = await _productionUnitScheduleModelRepository.GetByProductionUnitIdAsync(currentProductionUnit.Id, cancellationToken);
        terminalRoutineContext.SetIfNotNull(DataKey.Schedule, currentSchedule);
        terminalRoutineContext.SetIfNotNull(DataKey.ScheduleId, currentSchedule?.Id);

        // Fetch the production process (and its steps)
        var productionProcessId = currentSchedule?.GetCurrentTask()?.ProductionScheduleId;
        if (productionProcessId.HasValue)
        {
            var productionProcess = await _productionProcessModelRepository.GetByIdAsync(productionProcessId.Value, cancellationToken);
            terminalRoutineContext.SetIfNotNull(DataKey.ProductionProcess, productionProcess);
            terminalRoutineContext.SetIfNotNull(DataKey.ProductionProcessId, productionProcess?.Id);
        }

        // Fetch the order (and its progress)
        var orderId = currentSchedule?.GetCurrentTask()?.ProductionOrderId;
        if (orderId.HasValue)
        {
            var order = await _productionOrderModelRepository.GetByIdAsync(orderId.Value, cancellationToken);
            terminalRoutineContext.SetIfNotNull(DataKey.Order, order);
            terminalRoutineContext.SetIfNotNull(DataKey.OrderId, order?.Id);
        }

        // Prompt the worker to sign in and fetch the worker
        WorkerModel? worker = null;
        while (worker == null)
        {
            var workerNumber = _workerSignIn.RequestInput(new WorkerSignInInputRequest());
            worker = await _workerModelRepository.GetByNumberAsync(workerNumber ?? string.Empty, cancellationToken);
            if (worker == null)
                Console.WriteLine($"No worker for number '{workerNumber}' could be found. Try again.");
        }
        terminalRoutineContext.SetIfNotNull(DataKey.Worker, worker);
        terminalRoutineContext.SetIfNotNull(DataKey.WorkerId, worker.Id);
        terminalRoutineContext.SetIfNotNull(DataKey.WorkerNumber, worker.Number);
    }
}