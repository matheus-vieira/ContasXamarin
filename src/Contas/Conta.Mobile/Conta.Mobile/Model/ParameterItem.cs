namespace Conta.Mobile.Model
{
    public class ParameterItem
    {
        [Newtonsoft.Json.JsonProperty("id")]
        [Newtonsoft.Json.JsonConverter(typeof(Helpers.ParseStringConverter))]
        public long Id { get; set; }

        [Newtonsoft.Json.JsonProperty("nome")]
        public string Name { get; set; }
    }
}
