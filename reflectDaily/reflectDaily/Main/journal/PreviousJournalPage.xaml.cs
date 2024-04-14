using reflectDaily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main.journal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreviousJournalPage : ContentPage
	{
		private DateTime selectedDate;
		public PreviousJournalPage()
		{
			InitializeComponent();
            selectedDate = DateTime.Today;
            SetupCalendar();

        }
        private void SetupCalendar()
        {
            calendar.MaxDate = DateTime.Today;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetupCalendar();
        }


        private void ResultButton_Clicked(object sender, EventArgs e)
        {

            var userId = App.loggedUserObj.Id;
            var enddate = selectedDate.AddDays(1);
            List<PlayerResponse> responses = App.databaseManager.GetResponsesByDate(selectedDate,enddate, userId);
            if(responses != null)
            {
                responses = App.databaseManager.AddQuestionDetailToPlayerResponses(responses);
            }
            else
            {
                DisplayAlert("No Response Found", "There is no response on selected date", "ok");

            }


            if (responses == null)
            {
                DisplayAlert("RESULT NOT FOUND", "The selected Date does not have any data. Please choose another date.", "OK");
            }
            else {
                Navigation.PushAsync(new JournalResultPage(selectedDate, responses));
            }
            
        }

            private void calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
            {
            if (e.DateAdded != null && e.DateAdded.Count > 0)
            {
                selectedDate = e.DateAdded[0]; 
                                              
            }
        }

        private void GotoHomeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JournalPage());

        }
    }
}