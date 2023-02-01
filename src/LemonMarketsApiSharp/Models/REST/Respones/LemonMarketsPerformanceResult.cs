using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsPerformanceResult
    {
        #region Properties
        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("isin_title")]
        public string IsinTitle { get; set; }

        [JsonProperty("profit")]
        public long Profit { get; set; }

        [JsonProperty("loss")]
        public long Loss { get; set; }

        [JsonProperty("quantity_bought")]
        public long QuantityBought { get; set; }

        [JsonProperty("quantity_sold")]
        public long QuantitySold { get; set; }

        [JsonProperty("quantity_open")]
        public long QuantityOpen { get; set; }

        [JsonProperty("opened_at")]
        public DateTimeOffset? OpenedAt { get; set; }

        [JsonProperty("closed_at")]
        public DateTimeOffset? ClosedAt { get; set; }

        [JsonProperty("fees")]
        public long Fees { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
