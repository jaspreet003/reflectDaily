using reflectDaily.AccountManagement;
using reflectDaily.Main;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

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
