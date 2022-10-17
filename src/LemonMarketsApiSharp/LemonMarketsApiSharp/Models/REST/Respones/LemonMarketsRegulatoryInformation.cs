using Newtonsoft.Json;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsRegulatoryInformation
    {
        #region Properties
        [JsonProperty("costs_entry")]
        public long CostsEntry { get; set; }

        [JsonProperty("costs_entry_pct")]
        public string CostsEntryPct { get; set; }

        [JsonProperty("costs_running")]
        public long CostsRunning { get; set; }

        [JsonProperty("costs_running_pct")]
        public string CostsRunningPct { get; set; }

        [JsonProperty("costs_product")]
        public long CostsProduct { get; set; }

        [JsonProperty("costs_product_pct")]
        public string CostsProductPct { get; set; }

        [JsonProperty("costs_exit")]
        public long CostsExit { get; set; }

        [JsonProperty("costs_exit_pct")]
        public string CostsExitPct { get; set; }

        [JsonProperty("yield_reduction_year")]
        public long YieldReductionYear { get; set; }

        [JsonProperty("yield_reduction_year_pct")]
        public string YieldReductionYearPct { get; set; }

        [JsonProperty("yield_reduction_year_following")]
        public long YieldReductionYearFollowing { get; set; }

        [JsonProperty("yield_reduction_year_following_pct")]
        public string YieldReductionYearFollowingPct { get; set; }

        [JsonProperty("yield_reduction_year_exit")]
        public long YieldReductionYearExit { get; set; }

        [JsonProperty("yield_reduction_year_exit_pct")]
        public string YieldReductionYearExitPct { get; set; }

        [JsonProperty("estimated_holding_duration_years")]
        public long EstimatedHoldingDurationYears { get; set; }

        [JsonProperty("estimated_yield_reduction_total")]
        public long EstimatedYieldReductionTotal { get; set; }

        [JsonProperty("estimated_yield_reduction_total_pct")]
        public string EstimatedYieldReductionTotalPct { get; set; }

        [JsonProperty("KIID")]
        public string Kiid { get; set; }

        [JsonProperty("legal_disclaimer")]
        public string LegalDisclaimer { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
