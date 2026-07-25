using System.Reflection;

namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Builder class for constructing <see cref="ShopfloorCommandReceiverConfiguration"/> instances.
/// <para>
/// This class provides a fluent interface for configuring the shopfloor command receiver.
/// </para>
/// </summary>
/// <remarks>
/// This builder is used in the DI configuration to set up the receiver settings.
/// It is typically used within the
/// <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/> method call.
/// <para>
/// Example usage:
/// <code>
/// services.AddShopfloorCommandReceiver(builder => builder
///     .ScanInAssemblies(typeof(StartProductionCommandHandler).Assembly));
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandReceiverConfiguration"/>
/// <seealso cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/>
public sealed class ShopfloorCommandReceiverConfigurationBuilder
{
    private readonly ShopfloorCommandReceiverConfiguration _configuration = new();

    /// <summary>
    /// Specifies the assemblies to scan for command handlers.
    /// <para>
    /// The specified assemblies will be scanned for classes implementing
    /// <see cref="IShopfloorCommandHandler{TCommand}"/>. Each matching class will be
    /// automatically registered with the DI container.
    /// </para>
    /// </summary>
    /// <param name="assemblies">The assemblies to scan for command handlers.</param>
    /// <returns>The builder instance for method chaining.</returns>
    public ShopfloorCommandReceiverConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _configuration.Assemblies = assemblies;
        return this;
    }

    /// <summary>
    /// Builds the <see cref="ShopfloorCommandReceiverConfiguration"/> instance with the configured settings.
    /// <para>
    /// This method is internal and is called by the DI configuration extension method.
    /// </para>
    /// </summary>
    /// <returns>A configured <see cref="ShopfloorCommandReceiverConfiguration"/> instance.</returns>
    internal ShopfloorCommandReceiverConfiguration Build()
    {
        return _configuration;
    }
}