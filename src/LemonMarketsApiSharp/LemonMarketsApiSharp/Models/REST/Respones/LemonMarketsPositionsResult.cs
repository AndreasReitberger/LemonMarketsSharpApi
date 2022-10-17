using Newtonsoft.Json;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsPositionsResult
    {
        #region Properties
        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("isin_title")]
        public string IsinTitle { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("buy_price_avg")]
        public long BuyPriceAvg { get; set; }

        [JsonProperty("estimated_price_total")]
        public long EstimatedPriceTotal { get; set; }

        [JsonProperty("estimated_price")]
        public long EstimatedPrice { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
