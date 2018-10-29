using Conta.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Conta.Mobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage ()
		{
			InitializeComponent ();
		}
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) ||
                string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                EntryError("Login failed");
                return;
            }

            var user = new User
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            var btn = (Button)sender;

            btn.IsEnabled = false;
            loginActivityIndicator.IsRunning = true;

            var result = await Api.Login(user);

            btn.IsEnabled = true;
            loginActivityIndicator.IsRunning = false;

            if (!result.Ok)
            {
                EntryError("Login failed => " + result.Message);
                return;
            }

            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }

        private void EntryError(string msg)
        {
            messageLabel.Text = msg;
            passwordEntry.Text = string.Empty;
        }
    }
}