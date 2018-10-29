namespace Conta.Mobile.Helpers
{
    public class ParseStringConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(System.Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type t, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == Newtonsoft.Json.JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (long.TryParse(value, out long l))
            {
                return l;
            }
            throw new System.Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object untypedValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
