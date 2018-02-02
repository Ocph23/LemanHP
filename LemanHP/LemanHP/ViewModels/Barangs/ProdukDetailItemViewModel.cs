using LemanHP.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Barangs
{
    public class ProdukDetailItemViewModel:BaseViewModel
    {
        private Produk _produk;

        public ProdukDetailItemViewModel(Produk item)
        {
            Title = item.Nama;
            this.produk = item;
            CommandBeli = new Command(async (x) => await BeliActionAsync());
        }

        public Produk produk {
            get
            {
                return _produk;
            }
            set
            {
                SetProperty(ref _produk, value);
            }


        }
        public Command CommandBeli { get; private set; }

        private async Task BeliActionAsync()
        {
            var barang = (Barang)this.produk;
            if (barang.Stock >= 1)
            {
                var cart = new CartItem();
                cart.SetBarang(barang);
                bool result = await CartDataStore.AddItemAsync(cart);
                if(result)
                {
                    MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                    {
                        Message = "Ditambahkan ke keranjang",
                        Cancel = "Ok",
                        Title = "Success",
                     
                    }, "message");
                }
            }
            else
            {
                MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                {
                    Message = "Stok Habis",
                    Cancel = "Batal",
                    Title = "Error",
                   
                }, "message");
            }

          
        }


    }
}