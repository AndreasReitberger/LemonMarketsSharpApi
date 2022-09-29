using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsVenueResult
    {
        #region Properties
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("mic")]
        public string Mic { get; set; }

        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty("tradable")]
        public bool Tradable { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("opening_hours")]
        public LemonMarketsOpeningHours OpeningHours { get; set; }

        [JsonProperty("opening_days")]
        public List<DateTimeOffset> OpeningDays { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
