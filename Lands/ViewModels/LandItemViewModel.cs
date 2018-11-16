
namespace Lands.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Lands.Views;
    using Models;

    public class LandItemViewModel : Land
    {
       
        #region Commands
        public ICommand SelectLandCommand
        {
            get 
            {
                return new RelayCommand(SelectLand);
            }
           
        }

        private async void SelectLand()
        {
            MainViewModel.GetInstance().Land = new LandViewModel(this);
            await App.Navigator.PushAsync(new LandTabbedPage());
            //await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());
        }

        #endregion
    }
}
