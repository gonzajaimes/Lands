
namespace Lands.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using Services;
    using Models;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;

    public class LandsViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiservice;

        #endregion

        #region Attributes
        private ObservableCollection<Land> _lands;
        private bool _isRefreshing;
        private string _filter;
        private List<Land> landsList;
        #endregion

        #region Properties

        public string Filter
        {
            get { return _filter; }
            set 
            { 
                SetValue(ref _filter, value);
                this.Search();
            }
            
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public ObservableCollection<Land> Lands
        {
            get { return _lands; }
            set { SetValue(ref _lands, value); }
        }

        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiservice = new ApiService();
            this.LoadLands();
        }


        #endregion

        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiservice.CheckConnection();


            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     connection.Message,
                     "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiservice.GetList<Land>(
                           "http://restcountries.eu",
                           "/rest",
                           "/v2/all");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     response.Message,
                     "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            this.landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;
        }

        private void Search()
        {
           if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<Land>(this.landsList);
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) || 
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }

        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }

        }



        #endregion

    }
}
