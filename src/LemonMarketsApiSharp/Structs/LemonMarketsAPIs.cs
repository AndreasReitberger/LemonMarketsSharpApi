using System;

namespace AndreasReitberger.API.Structs
{
    public struct LemonMarketsAPIs
    {
        [Obsolete("Use DevBrokerageAPI instead")]
        public static string MarketDataAPI => "https://data.lemon.markets/v1/";
        public static string LiveTradingAPI => "https://trading.lemon.markets/v1";
        public static string LiveStreamingAPI => "https://realtime.lemon.markets/v1/";
        [Obsolete("Use DevBrokerageAPI instead")]
        public static string PaperTradingAPI => "https://paper-trading.lemon.markets/v1";
        public static string DevBrokerageApi => "https://dev-brokerage.lemon.markets/v1";
    }
}
