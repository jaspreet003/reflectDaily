using SQLite;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using reflectDaily.Model;
using System.Threading.Tasks; // For Task

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

        protected override async void OnAppearing() // Make sure this is async
        {
            base.OnAppearing();
            var con = new SQLiteAsyncConnection(App.DatabaseLocation);
            await con.CreateTableAsync<User>(); // Asynchronously ensure the table exists
        }

        private async void LoginButton_Clicked(object sender, EventArgs e) // Make this method async
        {
            var email = email.Text;
            var password = password.Text;

            // Input validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Failed", "Email and password cannot be empty.", "OK");
                return;
            }

            var con = new SQLiteAsyncConnection(App.DatabaseLocation);
            await con.CreateTableAsync<User>(); // Ensure the table exists

            // Query the database asynchronously for a user matching the email and password
            var user = await con.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

            if (user != null)
            {
                // Success
                await DisplayAlert("Login Success", "You have successfully logged in.", "OK");
                // Proceed to next page or operation
            }
            else
            {
                // Failure
                await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
            }
        }
    }
}
