using Newtonsoft.Json;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsAuthRespone
    {
        #region Properties
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
