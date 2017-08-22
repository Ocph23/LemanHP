using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using LemanHP.ViewModels;
using LemanHP.ViewModels.Accounts;

namespace LemanHP.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesananSaya : ContentPage
    {
        private Pembelian pembelian;
        private PesananSayaViewModel vm;

        public PesananSaya()
        {
            InitializeComponent();
           
        }

        public PesananSaya(Pembelian pembelian,Pelanggan pelanggan)
        {
            InitializeComponent();
            this.vm=new PesananSayaViewModel(pembelian,pelanggan);
            this.BindingContext = vm;
            this.pembelian = pembelian;
            

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var datepicker = (DatePicker)sender;
            vm.pembayaran.TanggalBayar = datepicker.Date;
        }
    }
}