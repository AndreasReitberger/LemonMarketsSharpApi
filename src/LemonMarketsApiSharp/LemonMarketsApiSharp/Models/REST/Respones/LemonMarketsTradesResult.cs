using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    /// <summary>
    /// https://docs.lemon.markets/market-data/historical-data#trades
    /// </summary>
    public partial class LemonMarketsTradesResult
    {
        #region Properties
        [JsonProperty("isin")]
        /// This is the International Securities Identification Number of the instrument you requested the quotes for.
        public string Isin { get; set; }

        [JsonProperty("p")]
        /// This is the Price the trade happened at
        public double Price { get; set; }  

        [JsonProperty("v")]
        /// This is the Volume for the trade (quantity)
        public long Volume { get; set; }

        [JsonProperty("t")]
        /// This is the Timestamp the trade occured at
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("mic")]
        /// This is the Market Identifier Code of the Trading Venue the trade occured at
        public string Mic { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
