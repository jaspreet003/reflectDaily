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
	public partial class JournalResultPage : ContentPage
	{
        private DateTime dateSelected;
        private List<PlayerResponse> responseList;

        public JournalResultPage (DateTime selectedDate, List<PlayerResponse> response)
		{
			InitializeComponent ();
            dateSelected = selectedDate;
            responseList = response;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            playerResponseList.ItemsSource = responseList;
        }

        private void AnotherDateButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void GotoHomeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }


    }
}