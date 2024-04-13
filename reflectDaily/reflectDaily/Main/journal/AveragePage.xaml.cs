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
    public partial class AveragePage : ContentPage
    {
        Dictionary<string, List<string>> responseDictionary;

        public AveragePage()
        {
            InitializeComponent();
            responseDictionary = new Dictionary<string, List<string>>();

            DateTime endDate = DateTime.Now.Date;
            DateTime startTime = endDate.AddDays(-2);

            var userId = App.loggedUserObj.Id;

            ProcessResponses(startTime, endDate, userId);
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
            Navigation.PushAsync(new AverageResultPage());
        }

        private void LastMonthButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AverageResultPage());
        }

        public void ProcessResponses(DateTime startDate, DateTime endDate, int userId)
        {
            var responses = App.databaseManager.GetResponsesByDate(startDate, endDate, userId);
            foreach (var response in responses)
            {
                UpdateResponseDictionary(response);
            }
        }

        public void UpdateResponseDictionary(PlayerResponse currentResponse)
        {
                    var currentQuestionId = currentResponse.QuestionId;

                    if (responseDictionary.ContainsKey(currentQuestionId))
                    {
                    responseDictionary[currentQuestionId].Add(currentResponse.SelectedOption);

                    Console.WriteLine(currentResponse.ToString());

                    Console.WriteLine("here goes existing item: ", responseDictionary.Count());

                    }
                    else
                    {
                    responseDictionary.Add(currentQuestionId, new List<string> { currentResponse.SelectedOption });
                    DisplayAlert("YOUR RESPONSES: ", currentResponse.ToString(), "OK");

                    Console.WriteLine(currentResponse.ToString());

            }
        }
       
    }
}