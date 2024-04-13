using Newtonsoft.Json;
using reflectDaily.AccountManagement;
using reflectDaily.Main;
using reflectDaily.Main.journal;
using reflectDaily.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static User loggedUserObj = null;
        public static DatabaseManager databaseManager = null;
        public static List<JournalQuestion> questions = new List<JournalQuestion>();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string databaseLocation)
        {
            //after this constructor, setup main.activity in both android and ios
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            DatabaseLocation = databaseLocation;

            databaseManager = new DatabaseManager(databaseLocation);

            LoadQuestionsFromJson();

        }
        private void LoadQuestionsFromJson()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("reflectDaily.questions.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<JournalQuestion>>(json);

                databaseManager.SaveQuestions(questions);
            }

        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
