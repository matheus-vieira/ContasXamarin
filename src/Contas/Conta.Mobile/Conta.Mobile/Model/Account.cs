namespace Conta.Mobile.Model
{
    public class Account
    {
        [Newtonsoft.Json.JsonProperty("origin")]
        public string Origin { get; set; }
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }
        [Newtonsoft.Json.JsonProperty("companyId")]
        public int CompanyId { get; set; }
        [Newtonsoft.Json.JsonProperty("dateCredit")]
        public string DateCredit { get; set; }
        [Newtonsoft.Json.JsonProperty("expirationDate")]
        public string ExpirationDate { get; set; }
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
        [Newtonsoft.Json.JsonProperty("grossValue")]
        public decimal GrossValue { get; set; }
        [Newtonsoft.Json.JsonProperty("netValue")]
        public decimal NetValue { get; set; }
        [Newtonsoft.Json.JsonProperty("penaltyValue")]
        public decimal PenaltyValue { get; set; }
        [Newtonsoft.Json.JsonProperty("discountValue")]
        public decimal DiscountValue { get; set; }
        [Newtonsoft.Json.JsonProperty("installment")]
        public int Installment { get; set; }
        [Newtonsoft.Json.JsonProperty("installments")]
        public int Installments { get; set; }
        [Newtonsoft.Json.JsonProperty("status")]
        public int Status { get; set; }
        [Newtonsoft.Json.JsonProperty("accountType")]
        public string AccountType { get; set; }
        [Newtonsoft.Json.JsonProperty("bankAccountId")]
        public int BankAccountId { get; set; }
        [Newtonsoft.Json.JsonProperty("bankAccount")]
        public string BankAccount { get; set; }
        [Newtonsoft.Json.JsonProperty("paymentMethodId")]
        public int PaymentMethodId { get; set; }
        [Newtonsoft.Json.JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }
        [Newtonsoft.Json.JsonProperty("billingMethodId")]
        public int BillingMethodId { get; set; }
        [Newtonsoft.Json.JsonProperty("billingMethod")]
        public object BillingMethod { get; set; }
        [Newtonsoft.Json.JsonProperty("accountCategoryId")]
        public int AccountCategoryId { get; set; }
        [Newtonsoft.Json.JsonProperty("accountCategory")]
        public string AccountCategory { get; set; }
        [Newtonsoft.Json.JsonProperty("centerResponsibilityId")]
        public int CenterResponsibilityId { get; set; }
        [Newtonsoft.Json.JsonProperty("centerResponsibility")]
        public string CenterResponsibility { get; set; }
        [Newtonsoft.Json.JsonProperty("transference")]
        public bool Transference { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string StatusText => Status == 0 ? "Open Bill" : "Paid Bill";

        public override string ToString()
        {
            return $"Historic: {Description} Amount: {GrossValue:C} Paid: {NetValue:C} Status {StatusText}";
        }
    }
    public class CreateAccount
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public object Id { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("empresaId")]
        public long EmpresaId { get; set; } = 1036;

        [Newtonsoft.Json.JsonProperty("tipoConta")]
        public string TipoConta { get; set; } = "R";

        [Newtonsoft.Json.JsonProperty("categoriaContaId")]
        public long CategoriaContaId { get; set; }

        [Newtonsoft.Json.JsonProperty("centroResponsabilidadeId")]
        public long CentroResponsabilidadeId { get; set; }

        [Newtonsoft.Json.JsonProperty("formaPagamentoId")]
        public long FormaPagamentoId { get; set; }

        [Newtonsoft.Json.JsonProperty("formaCobrancaId")]
        public object FormaCobrancaId { get; set; } = null;

        [Newtonsoft.Json.JsonProperty("contaBancoId")]
        public long ContaBancoId { get; set; }

        [Newtonsoft.Json.JsonProperty("dataVencimento")]
        public System.DateTime DataVencimento { get; set; }

        [Newtonsoft.Json.JsonProperty("dataPagamento")]
        public object DataPagamento { get; set; }

        [Newtonsoft.Json.JsonProperty("historico")]
        public string Historico { get; set; }

        [Newtonsoft.Json.JsonProperty("valor")]
        public decimal Valor { get; set; }

        [Newtonsoft.Json.JsonProperty("valorPago")]
        public decimal ValorPago { get; set; }

        [Newtonsoft.Json.JsonProperty("valorDesconto")]
        public decimal ValorDesconto { get; set; }

        [Newtonsoft.Json.JsonProperty("valorMulta")]
        public decimal ValorMulta { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public AccountStatusEnum Status { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string StatusText => Status == AccountStatusEnum.ContaEmAberto ? "Open Bill" : "Paid Bill";

        public override string ToString()
        {
            return $"Historic: {Historico} Amount: {Valor:C} Paid: {ValorPago:C} Status {StatusText}";
        }
    }
}
