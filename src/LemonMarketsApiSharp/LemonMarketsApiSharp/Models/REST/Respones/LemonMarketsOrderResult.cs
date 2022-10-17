using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsOrderResult
    {
        #region Properties
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("regulatory_information")]
        public LemonMarketsRegulatoryInformation RegulatoryInformation { get; set; }

        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("expires_at")]
        public DateTimeOffset ExpiresAt { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("stop_price")]
        public object StopPrice { get; set; }

        [JsonProperty("limit_price")]
        public object LimitPrice { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("estimated_price")]
        public long EstimatedPrice { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("idempotency")]
        public string Idempotency { get; set; }

        [JsonProperty("charge")]
        public long Charge { get; set; }

        [JsonProperty("chargeable_at")]
        public DateTimeOffset ChargeableAt { get; set; }

        [JsonProperty("key_creation_id")]
        public string KeyCreationId { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
