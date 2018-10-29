using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conta.Mobile
{
    public partial class MainPage : ContentPage
    {
        public bool IsLoading { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFilterButtonClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            btn.IsEnabled = false;
            contasActivityIndicator.IsVisible = true;
            contasActivityIndicator.IsRunning = true;
            result.IsVisible = false;

            var apiResult = await Api.GetContas(dateEntry.Date);
            var accountStatement = apiResult.Response.AccountStatement;

            previousBalanceValue.Text = string.Format("Previous Balance: {0:C}", accountStatement.PreviousBalance);
            currentBalance.Text = string.Format("Current Balance: {0:C}", accountStatement.CurrentBalance);

            accountsReceivable.Text = $"Accounts Receivable: {accountStatement.AccountsReceivable}";
            accountsPayable.Text = $"Accounts Payable: {accountStatement.AccountsPayable}";

            var list = new ObservableCollection<Model.Account>();
            foreach (var acc in accountStatement.Accounts)
                list.Add(acc);
            accountsListView.ItemsSource = list;

            btn.IsEnabled = true;
            result.IsVisible = true;
            contasActivityIndicator.IsVisible = false;
            contasActivityIndicator.IsRunning = false;
        }


        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Api.IsUserLoggedIn = false;
            Api.AuthAccessToken = string.Empty;

            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private async void OnNovaContaButtonClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new NewAccountPage(), this);
            await Navigation.PopAsync();
        }
    }
}
