using DandyMediator;
using DandyMediator.Queries;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Factories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.UseCases;
using Mes.Shopfloor.Shared.Messaging.Consumer;
using Mes.Shopfloor.Shared.ProductionManagement.Analysis.Events;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Consumers;

internal sealed class ProdUnitStatusProdUnitStateChangedConsumer(
    IMediator _mediator,
    IUnitOfWork _unitOfWork,
    IProductionUnitStatusFactory _factory) : IConsumer<ProdUnitStateChangedEvent>
{
    public async Task<ConsumerResult> HandleAsync(ProdUnitStateChangedEvent message, CancellationToken cancellationToken)
    {
        // Dismiss event if the production unit doesn't exist.
        var query = new ProductionUnitExists.Query(message.ProductionUnitId);
        var reponse = await _mediator.SendAsync<ProductionUnitExists.Query, bool>(query, cancellationToken);
        if (!reponse.Data)
            return ConsumerResult.Nack();

        // Dismiss event if the target state doesn't exist.
        var state = await _unitOfWork.Repository<IProductionUnitStateRepository>().GetByIdAsync(message.ProductionUnitId, cancellationToken);
        if (state == null)
            return ConsumerResult.Nack();

        // Get or create a new status. If both fail, the status could not be created.
        var statusRepository = _unitOfWork.Repository<IProductionUnitStatusRepository>();
        var status = await statusRepository.GetByIdEagerAsync(message.ProductionUnitId, cancellationToken);
        status ??= await _factory.CreateAsync(message.ProductionUnitId, cancellationToken);
        if (status == null)
            return ConsumerResult.Nack();

        // Set the new state and save.
        var statusState = ProductionUnitStatusState.FromState(message.ProductionUnitId, state, message.OccurredAtUtc);
        status.SetState(statusState);
        await statusRepository.SaveAsync(status, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ConsumerResult.Ack();
    }
}