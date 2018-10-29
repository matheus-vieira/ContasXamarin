namespace Conta.Mobile.Model
{
    public class Parameter
    {
        [Newtonsoft.Json.JsonProperty("parametros")]
        public Parametros Parameters { get; set; }
    }

    public class Parametros
    {
        [Newtonsoft.Json.JsonProperty("formaCobranca")]
        public System.Collections.Generic.List<ParameterItem> FormaCobranca { get; set; }

        [Newtonsoft.Json.JsonProperty("categoriaConta")]
        public System.Collections.Generic.List<ParameterItem> CategoriaConta { get; set; }

        [Newtonsoft.Json.JsonProperty("centroResponsabilidade")]
        public System.Collections.Generic.List<ParameterItem> CentroResponsabilidade { get; set; }

        [Newtonsoft.Json.JsonProperty("contaBanco")]
        public System.Collections.Generic.List<ParameterItem> ContaBanco { get; set; }
    }
}
