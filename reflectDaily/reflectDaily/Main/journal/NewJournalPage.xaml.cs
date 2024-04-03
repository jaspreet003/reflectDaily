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
	public partial class NewJournalPage : ContentPage
	{
		public NewJournalPage ()
		{
			InitializeComponent ();

            //setting label design
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

        List<JournalQuestion> questionList = new List<JournalQuestion>
        {
            new JournalQuestion
            {
                questionNumber = "Question - 1",
                questionDetail = "how many meals you eat today ?",
                options = new List<String> {
                "1 meal",
                "2 meal",
                "3 meal",
                "4 meal",
                }

            },

            new JournalQuestion
            {
                questionNumber = "Question - 2",
                questionDetail =  "How much you exercise today?",
                options = new List<String> {
                 "10 Min",
                "20 Min",
                "30 Min",
                "40 Min",
                "50 Min",
                "1 Hour",
                "1- 2 Hour",
                "More Than 2 Hours",
                "Did not practise today"
                }

            }
            

};
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Parent is NavigationPage navigationPage)
            {
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["primary"];
                navigationPage.BarTextColor = Color.White;
            }

            carouselQuestion.ItemsSource = questionList;

        }

        private void carouselQuestion_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {

        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            int nextPosition = carouselQuestion.Position + 1;

            if (nextPosition < ((carouselQuestion.ItemsSource as IEnumerable<object>).Count() - 1) )
            {
                carouselQuestion.Position = nextPosition;
            }
            else if(nextPosition < (carouselQuestion.ItemsSource as IEnumerable<object>).Count())
            {
                carouselQuestion.Position = nextPosition;
                var nextButton = sender as Button;
                nextButton.Text = "SUBMIT";
            }
            else
            {
                Navigation.PushAsync(new SuccessPage());
            }
        }

        private void PreviousButton_Clicked(object sender, EventArgs e)
        {
            int prevPosition = carouselQuestion.Position - 1;
            if (prevPosition > 0)
            {
                carouselQuestion.Position = prevPosition;
            }
            else
            {
                carouselQuestion.Position = prevPosition;

                var previousButton = sender as Button;
                previousButton.IsEnabled = false;
            }
        }
    }
}