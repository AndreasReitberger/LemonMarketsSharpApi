using AndreasReitberger.API.Structs;
using System;

namespace AndreasReitberger.API
{
    public partial class LemonMarketsClient
    {
        public class LemonMarketsConnectionBuilder
        {
            #region Instance
            readonly LemonMarketsClient _client = new();
            #endregion

            #region Methods

            public LemonMarketsClient Build()
            {
                return _client;
            }


            [Obsolete("Use WithDevBrokerageApi instead")]
            public LemonMarketsConnectionBuilder WithPaperTrading()
            {
                _client.Address = LemonMarketsAPIs.PaperTradingAPI;
                return this;
            }

            public LemonMarketsConnectionBuilder WithLiveTrading()
            {
                _client.Address = LemonMarketsAPIs.LiveTradingAPI;
                return this;
            }

            public LemonMarketsConnectionBuilder WithLiveStreaming()
            {
                _client.Address = LemonMarketsAPIs.LiveStreamingAPI;
                return this;
            }

            [Obsolete("Use WithDevBrokerageAPI instead")]
            public LemonMarketsConnectionBuilder WithMarketData()
            {
                _client.Address = LemonMarketsAPIs.MarketDataAPI;
                return this;
            }

            public LemonMarketsConnectionBuilder WithDevBrokerageApi()
            {
                _client.Address = LemonMarketsAPIs.DevBrokerageApi;
                return this;
            }

            public LemonMarketsConnectionBuilder WithWebAddress(string webAddress)
            {
                _client.Address = webAddress;
                return this;
            }

            public LemonMarketsConnectionBuilder WithApiKey(string apiKey)
            {
                _client.ApiKey = apiKey;
                return this;
            }

            public LemonMarketsConnectionBuilder WithWebAddressAndApiKey(string webAddress, string apiKey)
            {
                _client.Address = webAddress;
                _client.ApiKey = apiKey;
                return this;
            }

            #endregion
        }
    }
}
