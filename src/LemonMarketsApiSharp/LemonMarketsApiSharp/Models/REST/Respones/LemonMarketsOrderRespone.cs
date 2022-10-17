﻿using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsOrderRespone
    {
        #region Properties
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("results")]
        public LemonMarketsOrderResult Results { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
