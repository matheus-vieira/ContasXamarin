namespace Conta.Mobile.Model
{
    public partial class AccountStatement
    {
        [Newtonsoft.Json.JsonProperty("accounts")]
        public System.Collections.Generic.List<Account> Accounts { get; set; }

        [Newtonsoft.Json.JsonProperty("previousBalance")]
        public double PreviousBalance { get; set; }

        [Newtonsoft.Json.JsonProperty("currentBalance")]
        public double CurrentBalance { get; set; }

        [Newtonsoft.Json.JsonProperty("accountsReceivable")]
        public long AccountsReceivable { get; set; }

        [Newtonsoft.Json.JsonProperty("accountsPayable")]
        public long AccountsPayable { get; set; }
    }
}
