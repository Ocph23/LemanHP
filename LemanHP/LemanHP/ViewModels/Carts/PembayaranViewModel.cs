using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Carts
{
    public class PembayaranViewModel : BaseViewModel
    {
        private INavigation navigation;
        private ObservableCollection<CartItem> carts;
        private double _tBiaya;
        private double _tDiscount;

        public Pelanggan dataPelanggan { get; set; }
        private Pembelian pembelian { get; set; }
        public ObservableCollection<PembayaranItem> Datas { get; set; }
        public double TotalBiaya
        {
            get
            {
                return _tBiaya;
            }
            set
            {
                SetProperty(ref _tBiaya, value);
            }
        }
        public double TotalDiscount
        {
            get
            {
                return _tDiscount;
            }
            set
            {
                SetProperty(ref _tDiscount, value);
            }
        }
        public string KodePesanan { get; set; }
        public string Tanggal { get { return DateTime.Now.ToLocalTime().ToString(); } }

        public Command SetujuCommand { get; private set; }

        public PembayaranViewModel(INavigation navigation, ObservableCollection<CartItem> carts, Pelanggan dataPelanggan, Pembelian pem)
        {
            this.navigation = navigation;
            this.carts = carts;
            this.dataPelanggan = dataPelanggan;
            this.pembelian = pem;
            pembelian.PelangganId = dataPelanggan.Id;
            this.KodePesanan = Helpers.Helper.KodePemesanan();
            pembelian.KodePemesanan = KodePesanan;
            this.SetujuCommand = new Command(() => SetujuCommandAction());
            LoadAsync();
        }

        private async void SetujuCommandAction()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var result = await PembelianDataStore.InsertPembelian(this.pembelian);
                if (result)
                {
                    MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                    {
                        Message = "Pembelian Berhasil Dilakukan, Silahkan Cek Email Anda Dan segera lakukan pembayaran",
                        Cancel = "Ok",
                        Title = "Success"

                    }, "message");

                    this.carts.Clear();
                    await  navigation.PopToRootAsync();


                }else
                    MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                    {
                        Message = "Pembelian Gagal, Coba Ulangi Beberapa Saat Lagi",
                        Cancel = "Ok",
                        Title = "Error"

                    }, "message");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                {
                    Message = ex.Message,
                    Cancel = "Ok",
                    Title = "Error"

                }, "message");
            }finally
            {
                IsBusy = false;
            }
           
        }

        private async void LoadAsync()
        {
            Datas = new ObservableCollection<PembayaranItem>();
            foreach (var item in carts)
            {
                Datas.Add(new PembayaranItem { Nama = item.Nama, Count=item.Count, Berat = item.Berat, Kode = item.Id, Total = item.Total, disc = item.Total * item.Discount/100 });
            }
            var berat = Datas.Sum(O => O.Berat);
            double tarif = 40000;
            var biayaKirim = await CityDataStore.GetCost("jne",berat.ToString() , "114");
            if(biayaKirim!=null)
            {
                var value = biayaKirim.results.FirstOrDefault().costs.Where(O => O.service == "OKE").FirstOrDefault();
                tarif = value.cost.FirstOrDefault().value;
            }
            pembelian.Pengiriman = new Pengiriman { Berat = berat, Ekspedisi = "JNE", Kota = dataPelanggan.Telepon, Tarif = tarif, TanggalKirim=DateTime.Now };
            Datas.Add(new PembayaranItem { Nama = "Biaya Pengiriman", Total = tarif});
            var kodeValidasiPembayaran = Helpers.Helper.KodeValidasi();
            Datas.Add(new PembayaranItem { Nama = "Kode Validasi", Total = kodeValidasiPembayaran});
            pembelian.KodeValidasiPembayaran = kodeValidasiPembayaran;
            this.TotalBiaya = Datas.Sum(O => O.Total);
            this.TotalDiscount = Datas.Sum(O => O.disc);
           

        }
    }


    public class PembayaranItem
    {
        public string Nama { get; set; }
        public int Kode { get; set; }
        public double Total { get; set; }
        public double disc { get; set; }
        public double Berat { get; set; }
        public int Count { get; internal set; }

        public string KodeBarang
        {
            get
            {
                return string.Format("{0:D4}", Kode);
            }
        }

        public double Harga { get; internal set; }

        public double Biaya { get { return Count * Harga; } }
    }
}
