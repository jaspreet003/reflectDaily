using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using reflectDaily.Model;
using System.Threading.Tasks;
using System;
using reflectDaily.Main;
using Newtonsoft.Json;

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

            emailEntry.Text = "test2@gmail.com";
            passwordEntry.Text = "test123";
            

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
                //saving user as jsonObject
                App.loggedUserObj = user;
                var userJson = JsonConvert.SerializeObject(user);
                Application.Current.Properties["User"] = userJson;
                await Application.Current.SavePropertiesAsync();

                await Application.Current.SavePropertiesAsync();
                await Navigation.PushAsync(new HomePage());

            }
            else
            {
                await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
            }
        }
    }
}
