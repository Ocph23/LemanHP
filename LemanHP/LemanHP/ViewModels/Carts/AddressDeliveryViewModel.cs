using System.Collections.ObjectModel;
using LemanHP.Models;
using Xamarin.Forms;
using System;
using System.Diagnostics;
using LemanHP.Helpers;
using LemanHP.Services;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public Pelanggan DataPelanggan { get; set; }

        public AddressDeliveryViewModel(INavigation navigation, ObservableCollection<CartItem> carts)
        {
            this.navigation = navigation;
            this.carts = carts;
            DataPelanggan = new Pelanggan { TanggalDaftar = DateTime.Now };
            Cities = new ObservableCollection<City>();
            Provinces = new ObservableCollection<Province>();
            ExecuteLoadItemsCommand(null);
            CheckToken();
            LoginCommand = new Command((x) => LoginCommandAction());
            PembayaranCommand = new Command( async(x) => await PembayaranCommandActionAsync(), (x) => PembayaranValidate());
        }

        private async void CheckToken()
        {
            var main = await Helper.GetMainPageAsync();
            this.Token = main.Token;
        }

        private bool PembayaranValidate()
        {
           if(this.Token!=null || FormValid())
            {
                return true;
            }else
            {
                return false;
            }
        }

        private async Task PembayaranCommandActionAsync()
        {
            var page = new Views.Carts.PembayaranView();

            if (Token != null)
            {

                var pelanggan = await UserServiceDataStore.GetUserProfile(Token.Email);
                var pembelian = new Models.Pembelian { PelangganId = pelanggan.Id, Tanggal = DateTime.Now };
                foreach (var item in carts)
                {
                    pembelian.DetailPembelians.Add(new DetailPembelian { BarangId = item.Id });
                }
                page.BindingContext = new Carts.PembayaranViewModel(navigation, carts, pelanggan, pembelian);
                await navigation.PushAsync(page);
            }
            else
            {
             /*   var pembelian = new Models.Pembelian { Pelanggan = DataPelanggan, Tanggal = DateTime.Now };
                foreach (var item in carts)
                {
                    pembelian.DetailPembelians.Add(new DetailPembelian { BarangId = item.Id });
                }
                page.BindingContext = new Carts.PembayaranViewModel(navigation, carts, DataPelanggan, pembelian);
                await navigation.PushAsync(page);*/
            }

        }

        private async void LoginCommandAction()
        {
           
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

        public ICommand LoginCommand { get; private set; }
        public Command PembayaranCommand { get; private set; }
        public bool FormValid()
        {
            if (!string.IsNullOrEmpty(this.DataPelanggan.Alamat) && !string.IsNullOrEmpty(DataPelanggan.Email) && !string.IsNullOrEmpty(DataPelanggan.Nama) &&
                !string.IsNullOrEmpty(DataPelanggan.Telepon) && ProvinceSelected != null && CitySelected != null)
                return true;
            else
                return false;
        }
        public AuthenticationToken Token { get; private set; }

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