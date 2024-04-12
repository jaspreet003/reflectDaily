using Newtonsoft.Json;
using reflectDaily.Main.journal;
using reflectDaily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JournalPage : ContentPage
	{
		public JournalPage ()
		{
			InitializeComponent ();

            // Set the label to display the username
            if (Application.Current.Properties.TryGetValue("User", out var userJson) && userJson is string jsonString)
            {
                var user = JsonConvert.DeserializeObject<User>(jsonString);
                this.username.Text = user.Username;
            }

            // set the date
            DateTime thisDate = DateTime.Today;
            dateToday.Text = thisDate.ToString("D");
        }

        private void TodaysJournalButton_Clicked(object sender, EventArgs e)
        {
            // setup button style
            var newJournalButton = sender as Button;
            newJournalButton.BackgroundColor = (Color)Application.Current.Resources["secondary"];

            Navigation.PushAsync(new NewJournalPage());

        }

        private void PreviousJournalButton_Clicked(object sender, EventArgs e)
        {
            //setup button style
            var previousJournalButton = sender as Button;
            previousJournalButton.BackgroundColor = (Color)Application.Current.Resources["secondary"];

            Navigation.PushAsync(new PreviousJournalPage());
        }

        private void CheckAverageButton_Clicked(object sender, EventArgs e)
        {
            //setup button style
            var averageButton = sender as Button;
            averageButton.BackgroundColor = (Color)Application.Current.Resources["secondary"];

            Navigation.PushAsync(new AveragePage());
        }
    }
}