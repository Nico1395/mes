using DandyMediator;
using Mes.Shopfloor.Api.SharedKernel.Domain.Events;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal static class HandleOrderNotOnTrackV1
{
    private sealed class NotificationHandler : INotificationHandler<OrderNotOnTrackV1>
    {
        public async Task HandleAsync(OrderNotOnTrackV1 notification, CancellationToken cancellationToken)
        {
            // Check for whether the projected time collides with the next scheduled production task or maintenance appointment
            // Include quality checks in the time evaluation, if necessary.
        }
    }
}