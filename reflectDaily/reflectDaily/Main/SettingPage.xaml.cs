using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reflectDaily.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main
{


	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{      
        public SettingPage ()
		{
			InitializeComponent ();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var list = ThemeColour.getColorList ();
            colourList.ItemsSource = list;

        }
    

        private void ThemeColourTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            

        }
    }
}