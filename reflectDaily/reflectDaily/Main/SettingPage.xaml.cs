using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using reflectDaily.Main.journal;
using reflectDaily.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main
{


	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
        private string selectedThemeName;

        public SettingPage ()
		{
			InitializeComponent ();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            colourList.ItemsSource = ThemeColour.LoadThemes();

        }


        private void ThemeColourTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            if (sender is Frame frame && frame.BindingContext is Theme theme)
            {
                selectedThemeName = theme.Name;
            }
        }

        private void RandomThemeTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            var themes = ThemeColour.LoadThemes();
            var selectedTheme = themes.FirstOrDefault(t => t.Name == selectedThemeName);

            if (selectedTheme != null)
            {
                ThemeColour.ApplyTheme(selectedTheme);
            }
        }
    }
}