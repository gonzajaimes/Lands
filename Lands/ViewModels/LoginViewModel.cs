

namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
             
        #region Attributes
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password
        {
            get{ return _password;}
            set{ SetValue(ref _password, value);}
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
            this.IsRemembered = true;
            this.IsEnabled = true;
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
                    "Error",
                    "You must enter an email",
                    "Accept");
                return;

            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password",
                    "Accept");
                return;

            }
            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "gonzajaimes@hotmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Your e-mail or password is incorrect",
                    "Accept");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.DisplayAlert(
                    "Ok",
                    "Fuck Yeah!",
                    "Accept");
            return;
        }
        #endregion
        #region Methods
       
        #endregion
    }
}
