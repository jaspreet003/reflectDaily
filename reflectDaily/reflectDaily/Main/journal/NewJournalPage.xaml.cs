﻿using Newtonsoft.Json;
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
            titleView.Padding = new Thickness(0, 0, 0, 0); 
            NavigationPage.SetTitleView(this, titleView);

            carouselQuestion.ItemsSource = App.questions;




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


        private void carouselQuestion_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {

        }

        private  async void NextButton_Clicked(object sender, EventArgs e)
        {
            int nextPosition = carouselQuestion.Position + 1;

            if (nextPosition < ((carouselQuestion.ItemsSource as IEnumerable<object>).Count() - 1) )
            {

                nextButton.Text = "NEXT";

                nextButton.IsEnabled = false;

                var currentQuestion = App.questions[carouselQuestion.Position];
                UpdateOrCreateResponse(currentQuestion, PreviousOptionSelected.Text);

                carouselQuestion.Position = nextPosition;

                questionPosition++;

            }
            else if(nextPosition == (carouselQuestion.ItemsSource as IEnumerable<object>).Count()-1)
            {
                var nextButton = sender as Button;
                nextButton.Text = "SUBMIT";
                var currentQuestion = App.questions[carouselQuestion.Position];
                UpdateOrCreateResponse(currentQuestion, PreviousOptionSelected.Text);

                carouselQuestion.Position = nextPosition;

                questionPosition++;
            }
            else
            {
                await App.databaseManager.SaveResponsesToDatabase(responseList);
                 await Navigation.PushAsync(new SuccessPage());

                var currentQuestion = App.questions[carouselQuestion.Position];
                UpdateOrCreateResponse(currentQuestion, PreviousOptionSelected.Text);
            }

            if (questionPosition > 0)
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

            nextButton.Text = "NEXT";

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

            }



        }

        private void GotoHomeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
    }
}