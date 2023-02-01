namespace AndreasReitberger.API.Structs
{
    public struct LemonMarketsBankStatementTypes
    {
        public static string PayIn => "pay_in";
        public static string PayOut => "pay_out";
        public static string OrderBuy => "order_buy";
        public static string OrderSell => "order_sell";
        public static string EODBalance => "eod_balance";
        public static string Dividend => "dividend";
        public static string TaxRefunded => "tax_refunded";
    }
}
