using System;
using System.Collections.Generic;
using LemanHP.Models;
using Xamarin.Forms;
using Plugin.Media;
using LemanHP.Helpers;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;

namespace LemanHP.ViewModels.Accounts
{
    internal class PesananSayaViewModel:BaseViewModel
    {
        public PembelianView pembelian {
            get { return _pembelian; }
            set
            {
                SetProperty(ref _pembelian, value);
            }
        }
        public Pembayaran pembayaran {
            get {
                return _pembayaran;
            }
            set
            {
                SetProperty(ref _pembayaran, value);
            }
        }

        public Bank BankSelected
        {
            get { return _bank; }
            set
            {
                if(value!=null)
                {
                    this.pembayaran.BankTujuan = value.Nama;
                    this.pembayaran.RekeningTujuan = value.Nomor;
                }
                SetProperty(ref _bank, value);
            }
        }

        private byte[] _image;
        private Pembayaran _pembayaran;
        private Pelanggan _myAccount;
        private PembelianView _pembelian;
        private Bank _bank;
        private INavigation navigation;

        public PesananSayaViewModel(Pembelian pembelian,Pelanggan pelanggan, INavigation navigation)
        {
            this.navigation = navigation;
            this.pembelian = new PembelianView(pembelian, pelanggan);
            this.MyAccount = pelanggan;
            this.TakeFotoCommand = new Command(() => TakeFotoAction());
            this.KonfirmasiCommand = new Command(()=>KonfirmasiCommandAction(), ()=>KonfirmasiCommandValidation());
            this.pembayaran = new Pembayaran();
            this.Banks = new ObservableCollection<Bank>();
            this.Banks.Add(new Bank { Nama = "Mandiri", Nomor = "1234567" });
            this.Banks.Add(new Bank { Nama = "BCA", Nomor = "3455667" });
            this.Banks.Add(new Bank { Nama = "BRI", Nomor = "56458585" });
            pembayaran.Id = pembelian.Id;
            pembayaran.TanggalBayar = DateTime.Now;
        }

        private bool KonfirmasiCommandValidation()
        {
            return true;
        }

        private async void KonfirmasiCommandAction()
        {
            var result = await PembayaranDataStore.Konfirmasi(pembayaran);
            if(result)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Info",
                    Message = "Konfirmasi Pembayaran Sukses, Kami Akan Segera Memvaliasi pembayaran anda",
                    Cancel = "OK"
                }, "message");
               await navigation.PopToRootAsync();
            }
            else
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Info",
                    Message = "Konfirmasi pembayaran gagal, coba ulangi nanti",
                    Cancel = "OK"
                }, "message");
            }
        }

        public Command TakeFotoCommand { get; private set; }
        public byte[] PhotoImage {
            get { return _image; }
            set
            {
                pembayaran.BuktiTransfer = value;
                SetProperty(ref _image, value);
            }
        }

        public Command KonfirmasiCommand { get; private set; }
        public Pelanggan MyAccount {
            get { return _myAccount; }
            set
            {
                SetProperty(ref _myAccount, value);
            }
        }

        public ObservableCollection<Bank> Banks { get; private set; }

        private async void TakeFotoAction()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "No Camera",
                    Message = "Camera tidak tersedia",
                    Cancel = "OK"
                }, "message");
               
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "No Camera",
                    Message = "Camera tidak tersedia",
                    Cancel = "OK"
                }, "message");
                return;
            }else
            {
                var input = file.GetStream();
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    PhotoImage = ms.ToArray();
                }

                file.Dispose();
            }


        }
    }
}