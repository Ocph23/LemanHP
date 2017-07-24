using System.Collections.ObjectModel;
using LemanHP.Models;
using Xamarin.Forms;
using System;
using System.Diagnostics;
using LemanHP.Helpers;
using LemanHP.Services;
using System.Threading.Tasks;

namespace LemanHP.ViewModels.Carts
{
    internal class AddressDeliveryViewModel:BaseViewModel
    {
        private INavigation navigation;
        private ObservableCollection<CartItem> carts;
        private Province _province;
        private City _city;

        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<Province> Provinces { get; set; }

        public AddressDeliveryViewModel(INavigation navigation, ObservableCollection<CartItem> carts)
        {
            this.navigation = navigation;
            this.carts = carts;
            Cities = new ObservableCollection<City>();
            Provinces = new ObservableCollection<Province>();
            ExecuteLoadItemsCommand(null);
        }

        public Province ProvinceSelected
        {
            get
            {
                return _province;
            }
            set
            {
                if(value!=null && _province!=value)
                {
                    SetProperty(ref _province, value);
                    CitySelected = null;
                    SetCities(value);
                }
               
            }
        }


        public City CitySelected
        {
            get { return _city; }
            set
            {
                SetProperty(ref _city, value);
            }
        }


        private async void SetCities(Province value)
        {
            var res= await CityDataStore.GetCities(value.province_id);
            Cities.Clear();
            foreach(var item in res)
            {
                Cities.Add(item);
            }
        }
        
        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
               
                Provinces.Clear();
                var provincies = await CityDataStore.GetProvincies();
                foreach(var item in provincies)
                {
                   Provinces.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }



        public AddressDeliveryViewModel()
        {

        }

    }
}