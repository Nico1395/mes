using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandHubConnectionFactory"/> that creates
/// SignalR hub connections to the shopfloor command hub.
/// <para>
/// This factory creates connections using the configured hub base URL and shopfloor key,
/// with JSON protocol and automatic reconnection enabled.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
/// is called.
/// <para>
/// The factory validates that both the hub base URL and shopfloor key are configured
/// before attempting to create a connection. After establishing the connection, it
/// automatically registers the shopfloor with the hub using the configured key.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubConnectionFactory"/>
/// <seealso cref="ShopfloorCommandHubConnectionReceiverConfiguration"/>
internal sealed class ShopfloorCommandHubConnectionFactory(ShopfloorCommandHubConnectionReceiverConfiguration configuration) : IShopfloorCommandHubConnectionFactory
{
    /// <summary>
    /// Asynchronously creates and starts a new SignalR hub connection to version 1 of the shopfloor command hub.
    /// <para>
    /// This method:
    /// <list type="number">
    /// <item><description>Validates that required configuration is present</description></item>
    /// <item><description>Builds a SignalR connection with JSON protocol and automatic reconnection</description></item>
    /// <item><description>Starts the connection asynchronously</description></item>
    /// <item><description>Registers the shopfloor with the hub</description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the connected and registered hub connection.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the hub base URL or shopfloor key is not configured.
    /// </exception>
    /// <exception cref="HubException">
    /// Thrown if the connection cannot be started or if shopfloor registration fails.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Thrown if the operation is canceled via the cancellation token.
    /// </exception>
    public async Task<HubConnection> CreateV1Async(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(configuration.HubBaseUrl))
            throw new InvalidOperationException("The shopfloor command hub base URL was not configured.");

        if (string.IsNullOrWhiteSpace(configuration.ShopfloorKey))
            throw new InvalidOperationException("The shopfloor key was not configured.");

        var connection = new HubConnectionBuilder()
            .AddJsonProtocol()
            .WithAutomaticReconnect()
            .WithUrl($"{configuration.HubBaseUrl}/cmd/v1/")
            .Build();

        await connection.StartAsync(cancellationToken);
        await connection.InvokeAsync(ShopfloorCommandConstants.V1.Hub.RegisterShopfloor, configuration.ShopfloorKey, cancellationToken: cancellationToken);

        return connection;
    }
}