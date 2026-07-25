using Microsoft.AspNetCore.Builder;

namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Extension methods for mapping the shopfloor command hub in the ASP.NET Core pipeline.
/// </summary>
/// <remarks>
/// This class provides extension methods for <see cref="WebApplication"/> to simplify
/// the mapping of the shopfloor command hub route.
/// <para>
/// The hub is typically mapped to the "/cmd/" route, but this can be customized if needed
/// by modifying the source code.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHub"/>
/// <seealso cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Maps the shopfloor command hub to the "/cmd/" route in the ASP.NET Core pipeline.
    /// <para>
    /// This method should be called after <see cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/>
    /// has been called to register the required services.
    /// </para>
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to configure.</param>
    /// <remarks>
    /// This method maps the <see cref="ShopfloorCommandHub"/> to handle SignalR connections
    /// at the "/cmd/" route. Clients will connect to this route to send and receive
    /// shopfloor commands.
    /// <para>
    /// Example usage:
    /// <code>
    /// var app = builder.Build();
    /// app.MapShopfloorCommandHub();
    /// app.Run();
    /// </code>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the app parameter is null.</exception>
    public static void MapShopfloorCommandHub(this WebApplication app)
    {
        app.MapHub<ShopfloorCommandHub>("/cmd/");
    }
}