using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Factories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.UseCases;
using Mes.Shopfloor.Shared.Messaging.Consumer;
using Mes.Shopfloor.Shared.ProductionManagement.Scheduling.Events;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Consumers;

internal sealed class StatusStateChangedConsumer(
    IMediator _mediator,
    IUnitOfWork _unitOfWork,
    IProductionUnitStatusFactory _factory) : IConsumer<ProdUnitStateChangedEvent>
{
    public async Task<ConsumerResult> HandleAsync(ProdUnitStateChangedEvent message, CancellationToken cancellationToken)
    {
        // Dismiss event if the production unit doesn't exist.
        var query = new ProductionUnitExists.Query(message.ProductionUnitId);
        var response = await _mediator.SendAsync<ProductionUnitExists.Query, bool>(query, cancellationToken);
        if (response.ResultedInFalse())
            return ConsumerResult.Nack();

        // Dismiss event if the target state doesn't exist.
        var state = await _unitOfWork.Repository<IStateRepository>().GetByIdAsync(message.ProductionUnitId, cancellationToken);
        if (state == null)
            return ConsumerResult.Nack();

        // Get or create a new status. If both fail, the status could not be created.
        var statusRepository = _unitOfWork.Repository<IStatusRepository>();
        var status = await statusRepository.GetByIdEagerAsync(message.ProductionUnitId, cancellationToken);
        status ??= await _factory.CreateAsync(message.ProductionUnitId, cancellationToken);
        if (status == null)
            return ConsumerResult.Nack();

        // Set the new state and save.
        var statusState = StatusState.FromState(message.ProductionUnitId, state, message.OccurredAtUtc);
        status.SetState(statusState);
        await statusRepository.SaveAsync(status, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ConsumerResult.Ack();
    }
}