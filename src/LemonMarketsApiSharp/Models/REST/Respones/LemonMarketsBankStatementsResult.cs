using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsBankStatementsResult
    {
        #region Properties
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("isin_title")]
        public string IsinTitle { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
