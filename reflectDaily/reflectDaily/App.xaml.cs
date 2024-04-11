using reflectDaily.AccountManagement;
using reflectDaily.Main;
using reflectDaily.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static User loggedUserObj = null;
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
