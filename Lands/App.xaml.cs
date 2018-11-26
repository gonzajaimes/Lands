
namespace Lands
{
    using System;
    using Lands.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Helpers;
    using Lands.ViewModels;
    using Services;
    using Models;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master 
        { 
            get; 
            internal set; 
        }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            if(Settings.IsRemembered == "true")
            {
                var dataservice = new DataService();
                var token = dataservice.First<TokenResponse>(false);
                if (token != null && token.Expires > DateTime.Now)
                {
                    var user = dataservice.First<UserLocal>(false);
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Lands = new LandsViewModel();
                    MainPage = new MasterPage();
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }

            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());

            }

            
        }
        #endregion
        
        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
