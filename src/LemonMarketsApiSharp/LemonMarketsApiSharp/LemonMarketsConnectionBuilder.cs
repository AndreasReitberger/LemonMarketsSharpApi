using AndreasReitberger.API.Structs;

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
           
            public LemonMarketsConnectionBuilder WithMarketTrading()
            {
                _client.Address = LemonMarketsAPIs.MarketDataAPI;
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
