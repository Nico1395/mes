namespace Mes.Library.ShopfloorCommands.Connection;

internal static class ShopfloorCommandConstants
{
    public static class V1
    {
        public static class Hub
        {
            public const string RegisterShopfloor = "RegisterShopfloorV1";
            public const string SendCommand = "SendCommandV1";
            public const string BroadcastCommand = "BroadcastCommandV1";
            public const string Forward = "ForwardV1";
        }

        public static class Receiver
        {
            public const string ReceiveCommand = "ReceiveCommandV1";
        }
    }
}