using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsInstrumentResult
    {
        #region Properties
        [JsonProperty("isin")]
        public string Isin { get; set; }

        [JsonProperty("wkn")]
        public string Wkn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("venues")]
        public List<LemonMarketsVenueResult> Venues { get; set; } = new();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
