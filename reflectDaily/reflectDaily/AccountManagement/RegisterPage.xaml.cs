using reflectDaily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.AccountManagement
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            // Basic input validation
            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                await DisplayAlert("Input Validation", "Please fill in all fields.", "OK");
                return;
            }

            if (!email.Text.Contains("@"))
            {
                await DisplayAlert("Input Validation", "Please enter a valid email address.", "OK");
                return;
            }

            User user = new User()
            {
                Username = username.Text,
                Email = email.Text,
                Password = password.Text,
            };

            // Using the async API
            var db = new SQLiteAsyncConnection(App.DatabaseLocation);
            await db.CreateTableAsync<User>();
            var row = await db.InsertAsync(user);

            if (row > 0)
            {
                await DisplayAlert("Success", "Account Created", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Failed", "Check again", "OK");
            }
        }

        private void BacktoLoginButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync (new LoginPage());
        }
    }
}