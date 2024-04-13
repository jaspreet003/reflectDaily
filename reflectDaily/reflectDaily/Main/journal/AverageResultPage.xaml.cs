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
		public AverageResultPage (List<PlayerResponse> playerResponseList)
		{
			InitializeComponent ();
            foreach (PlayerResponse playerResponse in playerResponseList)
            {
                Console.WriteLine(playerResponse.ToString());


            }
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