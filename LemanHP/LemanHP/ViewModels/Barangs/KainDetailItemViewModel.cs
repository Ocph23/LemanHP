using System;
using System.Threading.Tasks;
using LemanHP.Models;
using Xamarin.Forms;

namespace LemanHP.ViewModels.Barangs
{
    public class KainDetailItemViewModel:BaseViewModel
    {
        private Kain _kain;

        public Kain kain { get { return _kain; }
            set {
                SetProperty(ref _kain, value);
            }
        }
        public Command CommandBeli { get; private set; }

        public KainDetailItemViewModel(Kain item)
        {
            Title = item.Nama;
            this.kain = item;
            CommandBeli = new Command(async (x) => await BeliActionAsync());
        }

        private async Task BeliActionAsync()
        {
            var barang = (Barang)this.kain;
            if(barang.Stock>=1)
            {
                var cart = new CartItem();
                cart.SetBarang(barang);
                bool result = await CartDataStore.AddItemAsync(cart);
                if (result)
                {
                    MessagingCenter.Send<Helpers.MessagingCenterAlert>(new Helpers.MessagingCenterAlert
                    {
                        Message = "Ditambahkan ke keranjang",
                        Cancel = "Ok",
                        Title = "Success",
                        OnCompleted = new Action(MessageComplete)

                    }, "message");
                }
            }
            else
            {
                MessagingCenter.Send(new Helpers.MessagingCenterAlert
                {
                    Message = "Stok Habis",
                    Cancel = "Batal",
                    Title = "Error",
                    OnCompleted = new Action(MessageComplete)
                   
                },"message");
            }
            
        }

        private void MessageComplete()
        {
            Helpers.Alert.Show("From Complete Message", "OK");
        }
    }
}