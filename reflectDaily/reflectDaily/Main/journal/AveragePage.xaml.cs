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
	public partial class AveragePage : ContentPage
	{
		public AveragePage ()
		{
			InitializeComponent ();
		}

        private void StartDateButton_Clicked(object sender, EventArgs e)
        {
            calendar.IsVisible = true;  
        }

        private void EndDateButton_Clicked(object sender, EventArgs e)
        {
            calendar.IsVisible = true;
        }

        private void CheckButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AverageResultPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void LastWeekAvgButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync (new AverageResultPage());
        }

        private void LastMonthButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AverageResultPage());
        }
    }
}