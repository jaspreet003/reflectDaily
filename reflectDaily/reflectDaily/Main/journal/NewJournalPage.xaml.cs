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
	public partial class NewJournalPage : ContentPage
	{
		public NewJournalPage ()
		{
			InitializeComponent ();
            var date = DateTime.Today.ToString("D");
            var titleView = new Label
            {
                Text = date,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            titleView.Margin = new Thickness(30, 0, 30, 0);

            NavigationPage.SetTitleView(this, titleView);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Parent is NavigationPage navigationPage)
            {
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["primary"];
                navigationPage.BarTextColor = Color.White; // Adjust as needed
            }
        }
    }
}