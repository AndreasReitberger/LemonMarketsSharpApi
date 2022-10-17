using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Respones
{
    public partial class LemonMarketsAccountInfoResults
    {
        #region Properties
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("billing_address")]
        public string BillingAddress { get; set; }

        [JsonProperty("billing_email")]
        public string BillingEmail { get; set; }

        [JsonProperty("billing_name")]
        public string BillingName { get; set; }

        [JsonProperty("billing_vat")]
        public string BillingVat { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("deposit_id")]
        public string DepositId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("iban_brokerage")]
        public string IbanBrokerage { get; set; }

        [JsonProperty("iban_origin")]
        public string IbanOrigin { get; set; }

        [JsonProperty("bank_name_origin")]
        public string BankNameOrigin { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("cash_to_invest")]
        public long CashToInvest { get; set; }

        [JsonProperty("cash_to_withdraw")]
        public long CashToWithdraw { get; set; }

        [JsonProperty("amount_bought_intraday")]
        public long AmountBoughtIntraday { get; set; }

        [JsonProperty("amount_sold_intraday")]
        public long AmountSoldIntraday { get; set; }

        [JsonProperty("amount_open_orders")]
        public long AmountOpenOrders { get; set; }

        [JsonProperty("amount_open_withdrawals")]
        public long AmountOpenWithdrawals { get; set; }

        [JsonProperty("amount_estimate_taxes")]
        public long AmountEstimateTaxes { get; set; }

        [JsonProperty("approved_at")]
        public DateTime? ApprovedAt { get; set; }

        [JsonProperty("trading_plan")]
        public string TradingPlan { get; set; }

        [JsonProperty("data_plan")]
        public string DataPlan { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("tax_allowance")]
        public long? TaxAllowance { get; set; }

        [JsonProperty("tax_allowance_start")]
        public DateTime? TaxAllowanceStart { get; set; }

        [JsonProperty("tax_allowance_end")]
        public DateTime? TaxAllowanceEnd { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
