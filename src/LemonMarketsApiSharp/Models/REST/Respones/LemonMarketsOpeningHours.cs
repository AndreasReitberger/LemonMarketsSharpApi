using Newtonsoft.Json;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsOpeningHours
    {
        #region Properties
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
