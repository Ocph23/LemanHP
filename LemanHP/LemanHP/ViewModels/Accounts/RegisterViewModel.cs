using LemanHP.Helpers;
using LemanHP.Models;
using LemanHP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Accounts
{
    public class RegisterViewModel:BaseViewModel
    {
        private Province _province;
        private City _city;
        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<Province> Provinces { get; set; }
        public Pelanggan DataPelanggan { get; set; }

        public RegisterViewModel()
        {
            Title = "register";
            DataPelanggan = new Pelanggan();
            Provinces = new ObservableCollection<Province>();
            Cities = new ObservableCollection<City>();
            RegisterCommand = new Command((x) => RegisterCommandActionAsync(x));
            ExecuteLoadItemsCommand();
        }

      
     

        private async void RegisterCommandActionAsync(object x)
        {

            if (CitySelected != null && DataPelanggan != null && !string.IsNullOrEmpty(DataPelanggan.Email) && !string.IsNullOrEmpty(DataPelanggan.Password) && !string.IsNullOrEmpty(DataPelanggan.Nama) &&
               !string.IsNullOrEmpty(DataPelanggan.Telepon))
            {
                DataPelanggan.Alamat = DataPelanggan.Alamat + Environment.NewLine +
                "Provinsi " + CitySelected.province + Environment.NewLine +
                "Kabupaten " + CitySelected.city_name + Environment.NewLine +
                "Kode Pos" + CitySelected.postal_code;
                var registered = await UserServiceDataStore.Register(DataPelanggan);
                if (registered)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Info",
                        Message = "Registrasi Sukses, Cek Email Anda",
                        Cancel = "OK"
                    }, "message");
                }
                else
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = "Registrasi Gagal",
                        Cancel = "OK"
                    }, "message");
                }
            }
            else
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Lengkapi Data Anda",
                    Cancel = "OK"
                }, "message");


        }

        private async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Provinces.Clear();
                var provincies = await CityDataStore.GetProvincies();
                foreach (var item in provincies)
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

        public Province ProvinceSelected
        {
            get
            {
                return _province;
            }
            set
            {
                if (value != null && _province != value)
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

        public ICommand RegisterCommand { get; private set; }

        private async void SetCities(Province value)
        {
            var res = await CityDataStore.GetCities(value.province_id);
            Cities.Clear();
            foreach (var item in res)
            {
                Cities.Add(item);
            }
        }

    }
}
