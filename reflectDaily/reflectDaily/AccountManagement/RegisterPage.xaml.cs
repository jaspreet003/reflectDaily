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

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
			User user = new User()
			{
				Username = username.Text,
				Email = email.Text,
				Password = password.Text,
			};

			using (SQLiteConnection con =  new SQLiteConnection(App.DatabaseLocation))
			{
				con.CreateTable<User> ();
				int row = con.Insert(user);
				con.Close ();

                if (row > 0)
                {
                    DisplayAlert("Success", "Account Created", "OK");
                    Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    DisplayAlert("Failed", "Check again", "OK");
                }
            }

        }
    }
}