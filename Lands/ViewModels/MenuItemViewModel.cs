

namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class MenuItemViewModel
    {
        #region Properties
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
           if (this.PageName == "LoginPage")
            {
                //Forget Settings when LogOut

                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;

                //Forget Token From MainViewModel
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = string.Empty;
                mainViewModel.TokenType = string.Empty;


                Application.Current.MainPage = new LoginPage();
            }
        }
        #endregion
    }
}
