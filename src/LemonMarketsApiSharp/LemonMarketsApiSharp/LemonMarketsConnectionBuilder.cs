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
