

namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Helpers;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiservice;

        #endregion

        #region Attributes
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private string _email;
        #endregion

        #region Properties
        public string Email
        {
            get { return _email; }
            set{ SetValue(ref _email,value);}
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetValue(ref _isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetValue(ref _isEnabled, value); }
        }


        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiservice = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            //usuario provisional para no andar digitando en desarrollo
            this.Email = "oreostudios@hotmail.com";
            //this.Password = "1234567";

        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;

            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;

            }
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;

            }

            var token = await this.apiservice.GetToken(
                               "https://landsapi1977.azurewebsites.net",
                               this.Email,
                               this.Password);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SomethingWrong,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    token.ErrorDescription,
                    Languages.Accept);
                this.Password = string.Empty;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;


        }
        #endregion

      
    }
}
