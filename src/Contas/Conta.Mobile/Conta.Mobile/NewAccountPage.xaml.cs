using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Conta.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAccountPage : ContentPage
    {
        public Model.Parametros Parameters { get; set; }

        public NewAccountPage()
        {
            InitializeComponent();
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Api.IsUserLoggedIn = false;
            Api.AuthAccessToken = string.Empty;

            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            btn.IsEnabled = false;
            newAccountActivityIndicator.IsVisible = true;
            newAccountActivityIndicator.IsRunning = true;

            var account = new Model.CreateAccount();
            var msg = string.Empty;
            try
            {
                msg = "Billing type";
                account.FormaPagamentoId = GetParameterItemId(billingTypeEntry, Parameters.FormaCobranca);
                msg = "Account category";
                account.CategoriaContaId = GetParameterItemId(accountCategoryEntry, Parameters.CategoriaConta);
                msg = "Responsability center";
                account.CentroResponsabilidadeId = GetParameterItemId(responsabilityCenterEntry, Parameters.CentroResponsabilidade);
                msg = "Bank account";
                account.ContaBancoId = GetParameterItemId(bankAccountEntry, Parameters.ContaBanco);
                msg = "Due date";
                account.DataVencimento = dueDateEntry.Date;
                msg = "Payment date";
                account.DataPagamento = paymentDateEntry.Date;
                msg = "Historic";
                account.Historico = historicEntry.Text;
                msg = "Amount";
                account.Valor = decimal.Parse(amountEntry.Text);

                msg = "Amount paid";
                if (string.IsNullOrEmpty(amountPaidEntry.Text))
                    account.ValorPago = 0;
                else
                    account.ValorPago = decimal.Parse(amountPaidEntry.Text);

                msg = "Discount";
                if (string.IsNullOrEmpty(discountEntry.Text))
                    account.ValorDesconto = 0;
                else
                    account.ValorDesconto = decimal.Parse(discountEntry.Text);

                msg = "Penalty";
                if (string.IsNullOrEmpty(penaltyEntry.Text))
                    account.ValorMulta = 0;
                else
                    account.ValorMulta = decimal.Parse(penaltyEntry.Text);

                msg = "Status";
                account.Status = statusEntry.SelectedIndex == 1
                    ? Model.AccountStatusEnum.ContaEmAberto
                    : Model.AccountStatusEnum.ContaPago;

                /*
                 id = fixo null
                empresaId = (int) valor fixo 1036
                tipoConta = (string) valor fixo "R"
                categoriaContaId = (int) Categoria selecionada pelo usuário
                centroResponsabilidadeId = (int) Centro de responsabilidade selecionado pelo usuário
                formaPagamentoId = (int) Forma de pagamento selecionado pelo usuário
                contaBancoId = (int) Conta bancária selecionada pelo usuário
                formaCobrancaId = fixo null
                dataVencimento = (string) Data de vencimento da conta no formato "2018-08-14"
                dataPagamento = (string) Data de pagamento da conta no formato "2018-08-14" ou null se a conta estiver em aberto
                historico = (string) Titulo da conta
                valor = (decimal) valor da conta
                valorPago = (decimal) valor pago - se a conta ainda não foi paga, enviar 0
                valorDesconto = (decimal) valor desconto - se a conta não tiver desconto, enviar 0
                valorMulta = (decimal) valor multa - se a conta não tiver multa, enviar 0
                status = (int)

                 */
            }
            catch (Exception)
            {
                btn.IsEnabled = true;
                newAccountActivityIndicator.IsVisible = false;
                newAccountActivityIndicator.IsRunning = false;
                await DisplayAlert("Alert", "Erro ao preencher o cadastro => " + msg, "OK");
                return;
            }

            var apiResult = await Api.CriarConta(account);

            btn.IsEnabled = true;
            newAccountActivityIndicator.IsVisible = false;
            newAccountActivityIndicator.IsRunning = false;

            if (!string.IsNullOrEmpty(apiResult.Message))
            {
                await DisplayAlert("Alert", "Erro ao preencher o cadastro => " + apiResult.Response, "OK");
                return;
            }

            OnBackButtonClicked(sender, e);
        }

        private async void NewAccount_Appearing(object sender, EventArgs e)
        {
            var apiResult = await Api.Cadastros();

            Parameters = apiResult.Response.Parameters;

            BuildPicker(billingTypeEntry, Parameters.FormaCobranca);
            BuildPicker(accountCategoryEntry, Parameters.CategoriaConta);
            BuildPicker(responsabilityCenterEntry, Parameters.CentroResponsabilidade);
            BuildPicker(bankAccountEntry, Parameters.ContaBanco);

            statusEntry.Items.Add("Open bill");
            statusEntry.Items.Add("Paid Bill");
        }

        private long GetParameterItemId(Picker picker, List<Model.ParameterItem> parameterItems)
        {
            var parameterItem = parameterItems.FirstOrDefault(i => i.Name == (string)picker.SelectedItem);
            return parameterItem.Id;
        }

        private void BuildPicker(Picker picker, List<Model.ParameterItem> parameterItems)
        {
            picker.Items.Clear();
            var list = parameterItems.Select(i => i.Name).ToList();
            foreach (var i in list)
                picker.Items.Add(i);
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }
    }
}