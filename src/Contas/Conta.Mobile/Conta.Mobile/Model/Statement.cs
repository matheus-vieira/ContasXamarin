namespace Conta.Mobile.Model
{
    public class Statement
    {
        [Newtonsoft.Json.JsonProperty("accountStatement")]
        public AccountStatement AccountStatement { get; set; }
    }
}
