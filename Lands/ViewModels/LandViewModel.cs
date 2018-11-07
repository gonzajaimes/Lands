
namespace Lands.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Models;

    public class LandViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Border> _borders;
        private ObservableCollection<Currency> _currencies;
        #endregion

        #region Properties

        public ObservableCollection<Border> Borders
        {
            get { return _borders; }
            set { this.SetValue(ref _borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { this.SetValue(ref _currencies, value); }
        }

        public Land Land
        {
            get;
            set;
        }


        #endregion


        #region Constructors
        public LandViewModel(Land land)
        {
            this.Land = land;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
        }


        #endregion

        #region Methods
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandsList
                           .FirstOrDefault(l => l.Alpha3Code == border);

                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name,
                    });
                }

            }

        }
        #endregion
    }
}
