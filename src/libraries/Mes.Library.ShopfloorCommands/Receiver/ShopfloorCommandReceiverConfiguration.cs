using System.Reflection;

namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Configuration class for the shopfloor command receiver.
/// <para>
/// This class holds the configuration settings required for the shopfloor command receiver,
/// particularly the assemblies to scan for command handlers.
/// </para>
/// </summary>
/// <remarks>
/// This configuration is used by the <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/>
/// method to discover and register command handlers.
/// <para>
/// The assemblies specified in this configuration will be scanned for classes that implement
/// <see cref="IShopfloorCommandHandler{TCommand}"/>. Each matching class will be registered
/// with the DI container as a transient service.
/// </para>
/// <para>
/// Configuration is typically set up using the
/// <see cref="ShopfloorCommandReceiverConfigurationBuilder"/> via the
/// <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/> extension method.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandReceiverConfigurationBuilder"/>
/// <seealso cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/>
/// <seealso cref="IShopfloorCommandHandler{TCommand}"/>
public sealed class ShopfloorCommandReceiverConfiguration
{
    /// <summary>
    /// Gets or sets the assemblies to scan for command handlers.
    /// <para>
    /// These assemblies will be scanned for classes implementing
    /// <see cref="IShopfloorCommandHandler{TCommand}"/>. Each matching class will be
    /// automatically registered with the DI container.
    /// </para>
    /// </summary>
    /// <value>An array of assemblies to scan. Defaults to an empty array.</value>
    /// <remarks>
    /// If this property is empty or null, no specific handlers will be registered,
    /// and all commands will be handled by the universal handler (implementing
    /// <see cref="IShopfloorCommandHandler"/>) which forwards commands to the RabbitMQ
    /// message bus.
    /// </remarks>
    public Assembly[] Assemblies { get; set; } = [];
}