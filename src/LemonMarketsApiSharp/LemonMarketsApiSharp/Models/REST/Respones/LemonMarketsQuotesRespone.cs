using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsQuotesRespone
    {
        #region Properties
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("results")]
        public List<LemonMarketsQuotesResult> Results { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
