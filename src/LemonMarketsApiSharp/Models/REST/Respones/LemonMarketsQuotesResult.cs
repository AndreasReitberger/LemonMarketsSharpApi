using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    /// <summary>
    /// https://docs.lemon.markets/market-data/historical-data#quotes
    /// </summary>
    public partial class LemonMarketsQuotesResult
    {
        #region Properties
        [JsonProperty("isin")]
        /// This is the International Securities Identification Number of the instrument you requested the quotes for.
        public string Isin { get; set; }

        [JsonProperty("b_v")]
        /// This is the Bid Volume for the Quote
        public double BidVolume { get; set; }

        [JsonProperty("a_v")]
        /// This is the Ask Volume for the Quote
        public double AskVolume { get; set; }

        [JsonProperty("b")]
        /// This is the Bid price for the Quote
        public double BidPrice { get; set; }

        [JsonProperty("a")]
        /// This is the Ask price for the Quote
        public double AskPrice { get; set; }

        [JsonProperty("t")]
        /// This is the timestamp the Quote occured at
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("mic")]
        /// This is the Market Identifier Code of the Trading Venue the Quote occured at
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
