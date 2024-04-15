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
	public partial class AverageResultPage : ContentPage
	{
        List<PlayerResponse> responseList = new List<PlayerResponse>();


        public AverageResultPage (List<PlayerResponse> playerResponseList)
		{
			InitializeComponent ();
            this.responseList = playerResponseList;

            /*foreach (PlayerResponse playerResponse in playerResponseList)
            {
                Console.WriteLine(playerResponse.ToString());


            }*/
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            playerResponseList.ItemsSource = responseList;  

        }

        private void BackToHomeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void AnotherDateButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}