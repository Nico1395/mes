namespace Mes.Library.ShopfloorCommands;

internal static class ShopfloorCommandConstants
{
    public static class V1
    {
        public static class Sender
        {
            public const string RegisterShopfloor = "RegisterShopfloorV1";
            public const string SendCommand = "SendCommandV1";
            public const string BroadcastCommand = "BroadcastCommandV1";
        }
        
        public static class Receiver
        {
            public const string ReceiveCommand = "ReceiveCommandV1";
        }
    }
}