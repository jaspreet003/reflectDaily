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
        List<PlayerResponse> responseList = new List<PlayerResponse>();
        int userId = 0;
        private DateTime startDate;
        private DateTime endDate;

        public AveragePage()
        {
            InitializeComponent();
            responseDictionary = new Dictionary<string, List<string>>();

            userId = App.loggedUserObj.Id;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void StartDateButton_Clicked(object sender, EventArgs e)
        {
            calendarEndDate.IsVisible = false;

            calendarStartDate.IsVisible = true;

            if (endDateButton.Text != "")
            {
                if (DateTime.TryParse(endDateButton.Text, out DateTime endDate))
                {
                    calendarStartDate.MaxDate = endDate; // Set the max date to restrict the calendar selection.

                }

            }
            
            
        }

        private void EndDateButton_Clicked(object sender, EventArgs e)
        {
            calendarStartDate.IsVisible = false;

            calendarEndDate.IsVisible = true;

            if (startDateButton.Text == "")
            {
                if (DateTime.TryParse(endDateButton.Text, out DateTime endDate))
                {
                    calendarEndDate.MaxDate = endDate; // Set the max date to restrict the calendar selection.

                }

            }


        }

        private void CheckButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AverageResultPage(responseList));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private  void LastWeekAvgButton_Clicked(object sender, EventArgs e)
        {
            var endDate = DateTime.Now.Date;
            var startDate = endDate.AddDays(-7);
            ProcessResponses(startDate, endDate, userId);

            if(responseList.Count > 0)
            {
                Navigation.PushAsync(new AverageResultPage(responseList));
            }
            else
            {
                DisplayAlert("NO RESULT FOUND", "There are no submitted response since last 7 days", "OK");
            }
        }

        private void LastMonthButton_Clicked(object sender, EventArgs e)
        {
            var endDate = DateTime.Now.Date;
            var startDate = endDate.AddDays(-30);
            ProcessResponses(startDate, endDate, userId);

            if (responseList.Count > 0)
            {
                Navigation.PushAsync(new AverageResultPage(responseList));
            }
            else
            {
                DisplayAlert("NO RESULT FOUND", "There are no submitted response since last 7 days", "OK");
            }
        }

        public void ProcessResponses(DateTime startDate, DateTime endDate, int userId)
        {

            var responses = App.databaseManager.GetResponsesByDate(startDate, endDate, userId);
            foreach (var response in responses)
            {
                UpdateResponseDictionary(response);
            }

            foreach (var response in responseDictionary)
            {
                var questionId = response.Key;

                var list = response.Value.ToList();
                var answerString = CheckFrequency(list);

                resultResponse(questionId, answerString);
            }

        }

        public void UpdateResponseDictionary(PlayerResponse currentResponse)
        {
             var currentQuestionId = currentResponse.QuestionId;

             if (responseDictionary.ContainsKey(currentQuestionId))
             {
                 responseDictionary[currentQuestionId].Add(currentResponse.SelectedOption);

             }
             else
             {
                 responseDictionary.Add(currentQuestionId, new List<string> { currentResponse.SelectedOption });
             }
            
            
        }

        public void resultResponse(string questionId, string answerString)
        {
            PlayerResponse playerResponse = new PlayerResponse();

            playerResponse.QuestionId = questionId;
            playerResponse.ResponseDate = DateTime.Now;
            playerResponse.SelectedOption = answerString;
            playerResponse.QuestionDetail = App.databaseManager.GetQuestionDetail(questionId);
            playerResponse.UserId = userId;

            
            responseList.Add(playerResponse);
        }

        public string CheckFrequency(List<string> answerList)
        {
            // Count the frequency of each answer
            var frequency = answerList.GroupBy(x => x)
                                      .ToDictionary(x => x.Key, x => x.Count());

            // Find the maximum frequency
            int maxFrequency = frequency.Values.Max();

            // Collect all items that have the maximum frequency
            var mostFrequent = frequency.Where(x => x.Value == maxFrequency)
                                        .Select(x => x.Key)
                                        .ToList();

            // Join the most frequent items into a single string, separated by commas
            string finalAnswerString = string.Join(", ", mostFrequent);

            return finalAnswerString;
        }


        private void calendarEndDate_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            if (e.DateAdded != null && e.DateAdded.Count > 0)
            {
                endDate = e.DateAdded[0];
                endDateButton.Text = startDate.ToString("dd/MM/yyyy");
            }

            calendarEndDate.IsVisible = false;
        }

        private void calendarStartDate_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            if (e.DateAdded != null && e.DateAdded.Count > 0)
            {
                startDate = e.DateAdded[0];
                startDateButton.Text = endDate.ToString("dd/MM/yyyy");

            }

            calendarStartDate.IsVisible = false;    
        }
    }
}