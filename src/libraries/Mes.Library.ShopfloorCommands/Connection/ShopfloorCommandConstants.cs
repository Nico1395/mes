namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Contains constant strings for shopfloor command SignalR hub method names.
/// <para>
/// These constants ensure consistent method naming across the command hub implementation
/// and help prevent errors from string typos.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and used within the library to maintain consistency between
/// hub method names and client invocations. Version 1 (V1) constants are used for the
/// current SignalR hub implementation.
/// </remarks>
internal static class ShopfloorCommandConstants
{
    /// <summary>
    /// Contains constants for version 1 of the shopfloor command hub.
    /// </summary>
    public static class V1
    {
        /// <summary>
        /// Contains constants for hub method names in version 1.
        /// </summary>
        public static class Hub
        {
            /// <summary>
            /// The name of the hub method used to register a shopfloor connection.
            /// <para>
            /// Called when a shopfloor connects to the hub to associate its key with the connection.
            /// </para>
            /// </summary>
            public const string RegisterShopfloor = "RegisterShopfloorV1";

            /// <summary>
            /// The name of the hub method used to send a command to a specific shopfloor.
            /// <para>
            /// Routes the command to the shopfloor identified by <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>.
            /// </para>
            /// </summary>
            public const string SendCommand = "SendCommandV1";

            /// <summary>
            /// The name of the hub method used to broadcast a command to all connected shopfloors.
            /// <para>
            /// Sends the command to all currently connected clients.
            /// </para>
            /// </summary>
            public const string BroadcastCommand = "BroadcastCommandV1";

            /// <summary>
            /// The name of the hub method used to forward a command to another shopfloor via RabbitMQ.
            /// <para>
            /// Used for <see cref="IShopfloorToShopfloorCommand"/> instances to enable
            /// cross-shopfloor communication through the message bus.
            /// </para>
            /// </summary>
            public const string Forward = "ForwardV1";
        }

        /// <summary>
        /// Contains constants for receiver method names in version 1.
        /// </summary>
        public static class Receiver
        {
            /// <summary>
            /// The name of the client method that receives commands.
            /// <para>
            /// Shopfloor clients register this method to receive incoming commands
            /// from the hub.
            /// </para>
            /// </summary>
            public const string ReceiveCommand = "ReceiveCommandV1";
        }
    }
}