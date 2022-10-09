using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    /// <summary>
    /// https://docs.lemon.markets/market-data/historical-data#get-ohlcx1
    /// </summary>
    public partial class LemonMarketsOHLCResult
    {
        #region Properties
        [JsonProperty("isin")]
        /// This is the International Securities Identification Number of the instrument you requested the quotes for.
        public string ISIN { get; set; }

        [JsonProperty("o")]
        /// This is the Open Price in the specific time period
        public double OpenPrice { get; set; }

        [JsonProperty("h")]
        /// This is the Highest Price in the specific time period
        public double HighestPrice { get; set; }

        [JsonProperty("l")]
        /// This is the Lowest Price in the specific time period
        public double LowestPrice { get; set; }

        [JsonProperty("c")]
        /// This is the Close Price in the specific time period
        public double ClosePrice { get; set; }

        [JsonProperty("v")]
        /// This is the aggegrated volume (Number of trades) of the instrument in the specific time period
        public long Volume { get; set; }

        [JsonProperty("pbv")]
        /// This is the Price by Volume (Sum of (quantity * last price)) of the instrument in the specific time period
        public double PriceByVolume { get; set; }

        [JsonProperty("t")]
        /// The timestamp of the beginning of the represented time interval.
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("mic")]
        /// This is the Market Identifier Code of the Trading Venue the OHLC data occured at
        public string MIC { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
