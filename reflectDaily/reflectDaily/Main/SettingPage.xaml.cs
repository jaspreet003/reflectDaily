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
        private Frame previouslySelectedFrame = null;


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

                if (previouslySelectedFrame != null)
                {
                    previouslySelectedFrame.BorderColor = Color.Transparent; 
                    previouslySelectedFrame.HasShadow = false;
                }

                frame.BorderColor = Color.Black; 
                frame.HasShadow = true;

                previouslySelectedFrame = frame;
            }
        }

        private void RandomThemeTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            selectedThemeName = "Random";

            if (previouslySelectedFrame != null)
            {
                previouslySelectedFrame.BorderColor = Color.Transparent; 
                previouslySelectedFrame.HasShadow = false;
                previouslySelectedFrame = null; 
            }

            if (sender is Frame randomFrame)
            {
                randomFrame.BorderColor = Color.Black;
                randomFrame.HasShadow = true; 

                previouslySelectedFrame = randomFrame; 
            }


        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            var themes = ThemeColour.LoadThemes();
            Theme selectedTheme;

            if (selectedThemeName == "Random")
            {
                // If "Random" is selected, fetch the random theme from the themes list
                selectedTheme = themes.FirstOrDefault(t => t.Name == "Random");
            }
            else
            {
                // Otherwise, find the theme by the selected name
                selectedTheme = themes.FirstOrDefault(t => t.Name == selectedThemeName);
            }

            if (selectedTheme != null)
            {
                ThemeColour.ApplyTheme(selectedTheme);
            }
            
        }
    }
}