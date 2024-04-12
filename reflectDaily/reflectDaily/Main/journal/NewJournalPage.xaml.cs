using Newtonsoft.Json;
using reflectDaily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main.journal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewJournalPage : ContentPage
	{

        private Button PreviousOptionSelected;
        private Button replacedBtn;

        private int questionPosition;
        List<JournalQuestion> questions;

        List<PlayerResponse> responseList = new List<PlayerResponse>();
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

            LoadQuestionsFromJson();

            


        }

        /*List<JournalQuestion> questionList = new List<JournalQuestion>
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
            

};*/
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Parent is NavigationPage navigationPage)
            {
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["primary"];
                navigationPage.BarTextColor = Color.White;
            }

            questionPosition = carouselQuestion.Position;

            if (questionPosition == 0)
            {
                previousButton.IsEnabled = false;
            }

            nextButton.IsEnabled = false;

            /*            carouselQuestion.ItemsSource = questionList;
            */

            //disabling previous button for first question

        }

        private void LoadQuestionsFromJson()
        {
            var assembly = typeof(NewJournalPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("reflectDaily.questions.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<JournalQuestion>>(json);

                carouselQuestion.ItemsSource = questions;
            }

        }

        private void carouselQuestion_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {

        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            int nextPosition = carouselQuestion.Position + 1;

            if (nextPosition < ((carouselQuestion.ItemsSource as IEnumerable<object>).Count() - 1) )
            {

                nextButton.IsEnabled = false;


                //minus 1 because nextPosition is already ++
                var currentQuestion = questions[carouselQuestion.Position];

/*                await DisplayAlert("Question", currentQuestion.ToString(), "ok");
*/              UpdateOrCreateResponse(currentQuestion, PreviousOptionSelected.Text);


                carouselQuestion.Position = nextPosition;

                questionPosition++;


            }
            else if(nextPosition < (carouselQuestion.ItemsSource as IEnumerable<object>).Count())
            {
                carouselQuestion.Position = nextPosition;
                var nextButton = sender as Button;
                nextButton.Text = "SUBMIT";
                questionPosition++;
            }
            else
            {
                await SaveResponsesToDatabase();
                await Navigation.PushAsync(new SuccessPage());
            }

            if(questionPosition > 0)
            {
                previousButton.IsEnabled = true;
            }


          
        }

        private void PreviousButton_Clicked(object sender, EventArgs e)
        {
            int prevPosition = carouselQuestion.Position - 1;
            var previousButton = sender as Button;

            if (prevPosition > 0)
            {
                carouselQuestion.Position = prevPosition;
                previousButton.IsEnabled = true;

            }
            else if (prevPosition == 0)
            {
                carouselQuestion.Position = prevPosition;
                previousButton.IsEnabled = false;
            }
            else {
                carouselQuestion.Position = prevPosition;
                previousButton.IsEnabled = true;

            }

            nextButton.IsEnabled = true;

        }

        private void OptionButton_Clicked(object sender, EventArgs e)
        {
            var optionButton = sender as Button;

            if (PreviousOptionSelected != null) {

                PreviousOptionSelected.BackgroundColor = (Color)Application.Current.Resources["base"];

            }

            optionButton.BackgroundColor = (Color)Application.Current.Resources["secondary"];
            PreviousOptionSelected = optionButton;

            nextButton.IsEnabled = true;

        }

        /*  private void UpdateOrCreateResponse(JournalQuestion currentQuestion, string selectedOption)
          {
              if (currentQuestion != null)
              {
                  int currentQuestionPostionObject = Convert.ToInt32(currentQuestion.questionNumber);
                  DisplayAlert("Save", currentQuestionPostionObject.ToString(), "OK");


                  if (responseList.Count > 0 && currentQuestionPostionObject >= 0)
                  {

                      var responseObject = responseList[0];

                      DisplayAlert("Save", responseObject.ToString(), "OK");


                      if (responseObject.QuestionId == currentQuestion.questionNumber)
                      {
                          responseObject.SelectedOption = selectedOption;
                          responseObject.ResponseDate = DateTime.Now.Date;

                          DisplayAlert("update", responseObject.ToString(), "OK");
                      }
                      else
                      {
                          // If not found, create a new response
                          var newResponse = new PlayerResponse
                          {
                              UserId = App.loggedUserObj.Id,
                              QuestionId = currentQuestion.questionNumber,
                              SelectedOption = selectedOption,
                              ResponseDate = DateTime.Now.Date
                          };

                          // Add the new response to the list
                          responseList.Add(newResponse);
                          DisplayAlert("Save", newResponse.ToString(), "OK");

                      }

                  }
                  else
                  {
                      var newResponse = new PlayerResponse
                      {
                          UserId = App.loggedUserObj.Id,
                          QuestionId = currentQuestion.questionNumber,
                          SelectedOption = selectedOption,
                          ResponseDate = DateTime.Now.Date
                      };

                      // Add the new response to the list
                      responseList.Add(newResponse);
                      DisplayAlert("Save First Time", newResponse.ToString(), "OK");  

                  }


              }
          }*/

        /*        private void UpdateOrCreateResponse(JournalQuestion currentQuestion, string selectedOption)
                {
                    if (currentQuestion != null)
                    {
                        if (responseList.Count > 0)
                        {
                            foreach (var response in responseList)
                            {
                                if (response.QuestionId == currentQuestion.questionNumber)
                                {
                                    response.SelectedOption = selectedOption;
                                    response.ResponseDate = DateTime.Now.Date;
                                    DisplayAlert("update", response.ToString(), "OK");


                                }
                                else
                                {
                                    // If not found, create a new response
                                    var newResponse = new PlayerResponse
                                    {
                                        UserId = App.loggedUserObj.Id,
                                        QuestionId = currentQuestion.questionNumber,
                                        SelectedOption = selectedOption,
                                        ResponseDate = DateTime.Now.Date
                                    };

                                    // Add the new response to the list
                                    responseList.Add(newResponse);
                                    DisplayAlert("Save", newResponse.ToString(), "OK");

                                }
                            }

                        }
                        else
                        {
                            var newResponse = new PlayerResponse
                            {
                                UserId = App.loggedUserObj.Id,
                                QuestionId = currentQuestion.questionNumber,
                                SelectedOption = selectedOption,
                                ResponseDate = DateTime.Now.Date
                            };

                            // Add the new response to the list
                            responseList.Add(newResponse);
                            DisplayAlert("Save First Time", newResponse.ToString(), "OK");

                        }

                    }
                }*/

        private void UpdateOrCreateResponse(JournalQuestion currentQuestion, string selectedOption)
        {
            if (responseList.Count > 0)
            {
                var response = responseList.FirstOrDefault(r => r.QuestionId == currentQuestion.questionNumber);

                if (response != null)
                {
                    response.SelectedOption = selectedOption;
                    response.ResponseDate = DateTime.Now.Date;
                    DisplayAlert("update", response.ToString(), "OK");

                }
                else
                {
                    var newResponse = new PlayerResponse
                    {
                        UserId = App.loggedUserObj.Id,
                        QuestionId = currentQuestion.questionNumber,
                        SelectedOption = selectedOption,
                        ResponseDate = DateTime.Now.Date
                    };

                    responseList.Add(newResponse);
                    DisplayAlert("Save", newResponse.ToString(), "OK");

                }
            }
            else
            {
                var newResponse = new PlayerResponse
                {
                    UserId = App.loggedUserObj.Id,
                    QuestionId = currentQuestion.questionNumber,
                    SelectedOption = selectedOption,
                    ResponseDate = DateTime.Now.Date
                };

                responseList.Add(newResponse);
                DisplayAlert("Save First Time", newResponse.ToString(), "OK");

            }



        }

        private async Task SaveResponsesToDatabase()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<PlayerResponse>();
                foreach (var response in responseList)
                {
                    var existingResponse = conn.Table<PlayerResponse>().FirstOrDefault(r => r.QuestionId == response.QuestionId && r.UserId == response.UserId && r.ResponseDate == response.ResponseDate);
                    if (existingResponse != null)
                    {
                        existingResponse.SelectedOption = response.SelectedOption;
                        conn.Update(existingResponse);
                        DisplayAlert("Update", "ho gya", "ok");
                    }
                    else
                    {
                        conn.Insert(response);
                        DisplayAlert("inserted", "ho gya", "ok");

                    }
                }
            }
        }

        /*private void SetPreviousResponse(PlayerResponse playerResponse)
        {

            if (playerResponse != null)
            {

                Button currentOptionSelected = new Button();
                PreviousOptionSelected.BackgroundColor = (Color)Application.Current.Resources["secondary"];


                currentOptionSelected.Text = playerResponse.SelectedOption;

*/
        /*                currentOptionSelected.Clicked += this.OptionButton_Clicked;
*//*
                PreviousOptionSelected = currentOptionSelected;
                replacedBtn = currentOptionSelected as Button;
            }


        }*/

    }
}