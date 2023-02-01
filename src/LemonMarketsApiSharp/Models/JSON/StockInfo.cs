using Newtonsoft.Json;

namespace AndreasReitberger.API.Models.JSON
{
    public partial class StockInfo
    {
        #region Properties
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("ISIN")]
        public string Isin { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
