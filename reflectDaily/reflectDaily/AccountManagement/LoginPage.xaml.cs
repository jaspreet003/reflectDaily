using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using reflectDaily.Model;
using System.Threading.Tasks;
using System;
using reflectDaily.Main; // For Task

namespace reflectDaily.AccountManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new RegisterPage());
            };
            registerLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing();
            var con = new SQLiteAsyncConnection(App.DatabaseLocation);
            await con.CreateTableAsync<User>(); 
        }

        private async void LoginButton_Clicked(object sender, EventArgs e) 
        {
            var email = emailEntry.Text;
            var password = passwordEntry.Text;

            // Input validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Failed", "Email and password cannot be empty.", "OK");
                return;
            }

            var con = new SQLiteAsyncConnection(App.DatabaseLocation);
            await con.CreateTableAsync<User>(); 

            var user = await con.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

            if (user != null)
            {
                await DisplayAlert("Login Success", "You have successfully logged in.", "OK");
                await Navigation.PushAsync(new HomePage());

            }
            else
            {
                await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
            }
        }
    }
}
