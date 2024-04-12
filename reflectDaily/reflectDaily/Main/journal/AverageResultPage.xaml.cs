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
		public AverageResultPage ()
		{
			InitializeComponent ();
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