using Mes.Libraries.RabbitMQ.Consumer;
using Mes.Shared.Events.Orders;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal static class HandleOrderNotOnTrackV1
{
    private sealed class NotificationHandler : IConsumer<OrderNotOnTrackV1>
    {
        public Task<ConsumerResult> HandleAsync(OrderNotOnTrackV1 message, CancellationToken cancellationToken)
        {
            // Check for whether the projected time collides with the next scheduled production task or maintenance appointment
            // Include quality checks in the time evaluation, if necessary.

            throw new NotImplementedException();
        }
    }
}