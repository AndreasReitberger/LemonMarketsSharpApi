using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsOrdersResult
    {
        #region Properties
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("isin_title")]
        public string IsinTitle { get; set; }

        [JsonProperty("expires_at")]
        public DateTimeOffset ExpiresAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("stop_price")]
        public long? StopPrice { get; set; }

        [JsonProperty("limit_price")]
        public long? LimitPrice { get; set; }

        [JsonProperty("estimated_price")]
        public long EstimatedPrice { get; set; }

        [JsonProperty("estimated_price_total")]
        public long EstimatedPriceTotal { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("executed_quantity")]
        public long ExecutedQuantity { get; set; }

        [JsonProperty("executed_price")]
        public long ExecutedPrice { get; set; }

        [JsonProperty("executed_price_total")]
        public long ExecutedPriceTotal { get; set; }

        [JsonProperty("activated_at")]
        public DateTimeOffset ActivatedAt { get; set; }

        [JsonProperty("executed_at")]
        public DateTimeOffset ExecutedAt { get; set; }

        [JsonProperty("rejected_at")]
        public DateTimeOffset? RejectedAt { get; set; }

        [JsonProperty("cancelled_at")]
        public DateTimeOffset? CancelledAt { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("charge")]
        public long Charge { get; set; }

        [JsonProperty("chargeable_at")]
        public DateTimeOffset ChargeableAt { get; set; }

        [JsonProperty("key_creation_id")]
        public string KeyCreationId { get; set; }

        [JsonProperty("key_activation_id")]
        public string KeyActivationId { get; set; }

        [JsonProperty("regulatory_information")]
        public LemonMarketsRegulatoryInformation RegulatoryInformation { get; set; }

        [JsonProperty("idempotency")]
        public string Idempotency { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
