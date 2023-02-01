using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Models.JSON
{
    public partial class GlobalStockIndicies
    {
        #region Properties
        [JsonProperty("GlobalIndicies")]
        public Dictionary<string, List<StockInfo>> GlobalIndicies { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
